using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public interface IClientService
    {
        Task<PagedResult<Client>> List(int page, int pageSize);

        Task Save(Client item);
        Task Delete(int Id);

        Task<Client> Get(int? Id);
        Task<bool> Includes(int Id);
    }
}