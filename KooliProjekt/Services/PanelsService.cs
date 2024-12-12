using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class PanelsService : IPanelsService
    {
        private readonly ApplicationDbContext _context;

        public PanelsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Panels
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Panel> Get(int id)
        {
            return await _context.Panels.FindAsync(id);
        }

        public async Task<PagedResult<Panel>> List(int page, int pageSize, PanelSearch search = null)
        {
            var query = _context.Panels.AsQueryable();

            search = search ?? new PanelSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Manufacturer.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.Manufacturer)
                .GetPagedAsync(page, pageSize);
        }



        public async Task Save(Panel list)
        {
            if (list.Id == 0)
            {
                _context.Panels.Add(list);
            }
            else
            {
                _context.Panels.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}
