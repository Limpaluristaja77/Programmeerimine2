using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IBuildingsService
    {
        Task<PagedResult<Buildings>> List(int page, int pageSize);
        Task<Buildings> Get(int id);
        Task Save(Buildings list);
        Task Delete(int id);
    }
}