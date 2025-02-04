using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public interface IServicesService
    {
        Task<PagedResult<Service>> List(int page, int pageSize);

        Task Save(Service item);
        Task Delete(int Id);

        Task<Service> Get(int? Id);
        Task<bool> Includes(int Id);
    }
}