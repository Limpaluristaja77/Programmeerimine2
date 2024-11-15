using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly ApplicationDbContext _context;

        public MaterialsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Material>> List(int page, int pageSize)
        {
            return await _context.Materials.GetPagedAsync(page, 5);
        }

        public async Task<Material> Get(int id)
        {
            return await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Material list)
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
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }
    }
}
