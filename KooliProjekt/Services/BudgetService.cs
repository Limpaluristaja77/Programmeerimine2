using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Budgets
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Budget> Get(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task<PagedResult<Budget>> List(int page, int pageSize, BudgetSearch search = null)
        {
            var query = _context.Budgets.AsQueryable();

            search = search ?? new BudgetSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.ClientId.ToString().Contains(search.Keyword));
            }

            return await query
                .Include(list => list.Client)
                .Include(list => list.Buildings)
                .Include(list => list.Services)
                .OrderBy(list => list.ClientId)
                .GetPagedAsync(page, pageSize);
        }



        public async Task Save(Budget list)
        {
            if (list.Id == 0)
            {
                _context.Budgets.Add(list);
            }
            else
            {
                _context.Budgets.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}
