using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IBuildingsService
    {
        Task<PagedResult<Buildings>> List(int page, int pageSize, BuildingSearch query = null);
        Task<Buildings> Get(int id);
        Task Save(Buildings list);
        Task Delete(int id);
    }
}