namespace KooliProjekt.WpfApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<Panel>>> List();
        Task<Result>Save(Panel list);
        Task<Result>Delete(int id);
    }
}