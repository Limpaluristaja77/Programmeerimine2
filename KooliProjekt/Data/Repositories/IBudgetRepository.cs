namespace KooliProjekt.Data.Repositories
{
    public interface IBudgetRepository
    {
        Task<Budget> Get(int id);
        Task<PagedResult<Budget>> List(int page, int pageSize);
        Task Save(Budget item);
        Task Delete(int id);
    }
}