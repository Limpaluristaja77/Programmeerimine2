namespace KooliProjekt.Data.Repositories
{
    public interface IMaterialsRepository
    {
        Task<Material> Get(int id);
        Task<PagedResult<Material>> List(int page, int pageSize);
        Task Save(Material item);
        Task Delete(int id);
    }
}