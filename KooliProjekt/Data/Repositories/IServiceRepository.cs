namespace KooliProjekt.Data.Repositories
{
    public interface IServiceRepository
    {
        Task<Service> Get(int id);
        Task<PagedResult<Service>> List(int page, int pageSize);
        Task Save(Service item);
        Task Delete(int id);
    }
}