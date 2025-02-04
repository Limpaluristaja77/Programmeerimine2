using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public interface IPanelsService
    {
        Task<PagedResult<Panel>> List(int page, int pageSize);

        Task Save(Panel item);
        Task Delete(int Id);

        Task<Panel> Get(int? Id);
        Task<bool> Includes(int Id);
    }
}