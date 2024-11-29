

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IBudgetRepository budgetRepository,
            IBuildingsRepository buildingsRepository,
            IClientRepository clientRepository,
            IMaterialsRepository materialsRepository,
            IPanelsRepository panelsRepository,
            IServiceRepository serviceRepository)
        {
            _context = context;

            BudgetRepository = budgetRepository;
            BuildingsRepository = buildingsRepository;
            ClientRepository = clientRepository;
            MaterialsRepository = materialsRepository;
            PanelsRepository = panelsRepository;
            ServiceRepository = serviceRepository;
        }

        public IBudgetRepository BudgetRepository { get; private set; }
        public IBuildingsRepository BuildingsRepository { get; private set; }
        public IClientRepository ClientRepository { get; private set; }
        public IMaterialsRepository MaterialsRepository { get; private set; }
        public IPanelsRepository PanelsRepository { get; private set; }
        public IServiceRepository ServiceRepository { get; private set; }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
