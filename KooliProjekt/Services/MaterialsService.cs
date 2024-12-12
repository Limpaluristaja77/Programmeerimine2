using KooliProjekt.Data;
using KooliProjekt.Search;
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

        public async Task Delete(int id)
        {
            await _context.Materials
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Material> Get(int id)
        {
            return await _context.Materials.FindAsync(id);
        }

        public async Task<PagedResult<Material>> List(int page, int pageSize, MaterialSearch search = null)
        {
            var query = _context.Materials.AsQueryable();

            search = search ?? new MaterialSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Name.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.Name)
                .GetPagedAsync(page, pageSize);
        }



        public async Task Save(Material list)
        {
            if (list.Id == 0)
            {
                _context.Materials.Add(list);
            }
            else
            {
                _context.Materials.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}
