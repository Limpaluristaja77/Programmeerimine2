using KooliProjekt.Data;
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

        public async Task<PagedResult<Budget>> List(int page, int pageSize)
        {
            return await _context.Budgets.GetPagedAsync(page, 5);
        }

        public async Task<Budget> Get(int id)
        {
            return await _context.Budgets.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Budget list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }
        }
    }
}