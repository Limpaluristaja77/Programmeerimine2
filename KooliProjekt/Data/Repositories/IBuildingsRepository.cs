namespace KooliProjekt.Data.Repositories
{
    public interface IBuildingsRepository
    {
        Task<Buildings> Get(int id);
        Task<PagedResult<Buildings>> List(int page, int pageSize);
        Task Save(Buildings item);
        Task Delete(int id);
    }
}