using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public interface IBudgetService
    {
        Task<PagedResult<Budget>> List(int page, int pageSize);
        Task<DbSet<Buildings>> GetBuildingsAsync();
        Task<DbSet<Client>> GetClientsAsync();
        Task<DbSet<Service>> GetServicesAsync();

        Task Save(Budget item);
        Task Delete(int Id);

        Task<Budget> Get(int? Id);
        Task<bool> Includes(int Id);
    }
}