using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IClientService
    {
        Task<PagedResult<Client>> List(int page, int pageSize, ClientSearch query = null);
        Task<Client> Get(int id);
        Task Save(Client list);
        Task Delete(int id);
    }
}