using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Get(int? id);
        Task Save(T instance);
        Task Delete(T instance);
        Task Delete(int? id);
        Task<PagedResult<T>> List(int page, int pageSize);
        Task<bool> Includes(int Id);
        Task<DbSet<Budget>> GetAllBudgets();
        Task<DbSet<Buildings>> GetAllBuildings();
        Task<DbSet<Client>> GetAllClients();
        Task<DbSet<Service>> GetAllServices();
        Task<DbSet<Panel>> GetAllPanels();
        Task<DbSet<Material>> GetAllMaterials();

    }

}