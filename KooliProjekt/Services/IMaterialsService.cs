using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public interface IMaterialsService
    {
        Task<PagedResult<Material>> List(int page, int pageSize);

        Task Save(Material item);
        Task Delete(int Id);

        Task<Material> Get(int? Id);
        Task<bool> Includes(int Id);
    }
}