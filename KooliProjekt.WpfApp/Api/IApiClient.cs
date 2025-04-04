namespace KooliProjekt.WpfApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<Panel>>> List();
        Task Save(Panel list);
        Task Delete(int id);
    }
}