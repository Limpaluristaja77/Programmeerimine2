namespace KooliProjekt.Data.Repositories
{
    public interface IPanelsRepository
    {
        Task<Panel> Get(int id);
        Task<PagedResult<Panel>> List(int page, int pageSize);
        Task Save(Panel item);
        Task Delete(int id);
    }
}