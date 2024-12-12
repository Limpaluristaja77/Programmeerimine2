using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IServicesService
    {
        Task<PagedResult<Service>> List(int page, int pageSize, ServiceSearch query = null);
        Task<Service> Get(int id);
        Task Save(Service list);
        Task Delete(int id);

        
    }
}


