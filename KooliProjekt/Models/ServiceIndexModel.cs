using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class ServiceIndexModel
    {
        public ServiceSearch Search { get; set; }
        public PagedResult<Service> Data { get; set; }
    }
}
