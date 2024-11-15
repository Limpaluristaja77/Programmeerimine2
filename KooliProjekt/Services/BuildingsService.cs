using KooliProjekt.Data;
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

        public async Task<PagedResult<Buildings>> List(int page, int pageSize)
        {
            return await _context.Buildings.GetPagedAsync(page, 5);
        }

        public async Task<Buildings> Get(int id)
        {
            return await _context.Buildings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Buildings list)
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
            var buildings = await _context.Buildings.FindAsync(id);
            if (buildings != null)
            {
                _context.Buildings.Remove(buildings);
                await _context.SaveChangesAsync();
            }
        }
    }
}