using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public interface IBuidlingsService
    {
        Task<PagedResult<Buildings>> List(int page, int pageSize);
        Task<DbSet<Panel>> GetPanelsAsync();
        Task<DbSet<Material>> GetMaterialsAsync();

        Task Save(Buildings item);
        Task Delete(int Id);

        Task<Buildings> Get(int? Id);
        Task<bool> Includes(int Id);
    }
}