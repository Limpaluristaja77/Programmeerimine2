using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IPanelsService
    {
        Task<PagedResult<Panel>> List(int page, int pageSize, PanelSearch query = null);
        Task<Panel> Get(int id);
        Task Save(Panel list);
        Task Delete(int id);
    }
}