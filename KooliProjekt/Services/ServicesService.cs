using KooliProjekt.Data;
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

        public async Task<PagedResult<Service>> List(int page, int pageSize)
        {
            return await _context.Services.GetPagedAsync(page, 5);
        }

        public async Task<Service> Get(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Service list)
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
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}

