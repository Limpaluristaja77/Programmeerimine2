using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Clients
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Client> Get(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<PagedResult<Client>> List(int page, int pageSize, ClientSearch search = null)
        {
            var query = _context.Clients.AsQueryable();

            search = search ?? new ClientSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Name.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.Name)
                .GetPagedAsync(page, pageSize);
        }

        

        public async Task Save(Client list)
        {
            if (list.Id == 0)
            {
                _context.Clients.Add(list);
            }
            else
            {
                _context.Clients.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        
    }
}
