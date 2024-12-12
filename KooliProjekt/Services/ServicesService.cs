using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext _context;

        public ServicesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Services
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Service> Get(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<PagedResult<Service>> List(int page, int pageSize, ServiceSearch search = null)
        {
            var query = _context.Services.AsQueryable();

            search = search ?? new ServiceSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Name.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.Name)
                .GetPagedAsync(page, pageSize);
        }



        public async Task Save(Service list)
        {
            if (list.Id == 0)
            {
                _context.Services.Add(list);
            }
            else
            {
                _context.Services.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}
