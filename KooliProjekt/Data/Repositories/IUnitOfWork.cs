
namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

        IBudgetRepository BudgetRepository { get; }
        IBuildingsRepository BuildingsRepository { get; }
        IClientRepository ClientRepository { get; }
        IMaterialsRepository MaterialsRepository { get; }
        IPanelsRepository PanelsRepository { get; }
        IServiceRepository ServiceRepository { get; }

    }
}
