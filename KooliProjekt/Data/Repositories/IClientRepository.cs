namespace KooliProjekt.Data.Repositories
{
    public interface IClientRepository
    {
        Task<Client> Get(int id);
        Task<PagedResult<Client>> List(int page, int pageSize);
        Task Save(Client item);
        Task Delete(int id);
    }
}