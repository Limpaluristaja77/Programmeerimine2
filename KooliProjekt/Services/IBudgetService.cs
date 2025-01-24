using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IBudgetService
    {
        Task<PagedResult<Budget>> List(int page, int pageSize, BudgetSearch query = null);
        Task<Budget> Get(int id);
        Task Save(Budget list);
        Task Delete(int id);
    }
}