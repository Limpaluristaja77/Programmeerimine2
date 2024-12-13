using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class BuildingsService : IBuildingsService
    {
        private readonly ApplicationDbContext _context;

        public BuildingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Buildings
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Buildings> Get(int id)
        {
            return await _context.Buildings.FindAsync(id);
        }

        public async Task<PagedResult<Buildings>> List(int page, int pageSize, BuildingSearch search = null)
        {
            var query = _context.Buildings.AsQueryable();

            search = search ?? new BuildingSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Name.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.Name)
                .GetPagedAsync(page, pageSize);
        }



        public async Task Save(Buildings list)
        {
            if (list.Id == 0)
            {
                _context.Buildings.Add(list);
            }
            else
            {
                _context.Buildings.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}
