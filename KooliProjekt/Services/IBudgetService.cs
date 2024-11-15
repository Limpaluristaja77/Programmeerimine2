using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IBudgetService
    {
        Task<PagedResult<Budget>> List(int page, int pageSize);
        Task<Budget> Get(int id);
        Task Save(Budget list);
        Task Delete(int id);
    }
}