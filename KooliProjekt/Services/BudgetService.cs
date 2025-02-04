using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IUnitOfWork _uof;
        public BudgetService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Budget> Get(int? Id)
        {
            return await _uof.BudgetRepository.Get(Id);
        }

        public async Task<DbSet<Buildings>> GetBuildingsAsync()
        {
            return await _uof.BudgetRepository.GetAllBuildings();
        }

        public async Task<DbSet<Client>> GetClientsAsync()
        {
            return await _uof.BudgetRepository.GetAllClients();
        }

        public async Task<DbSet<Service>> GetServicesAsync()
        {
            return await _uof.BudgetRepository.GetAllServices();
        }
        public async Task<bool> Includes(int Id)
        {
            return await _uof.BudgetRepository.Includes(Id);
        }

        public Task<PagedResult<Budget>> List(int page, int pageSize)
        {
            return _uof.BudgetRepository.List(page, pageSize);
        }

        public async Task Save(Budget item)
        {
            await _uof.BudgetRepository.Save(item);
        }
        public async Task Delete(int Id)
        {
            await _uof.BudgetRepository.Delete(Id);

        }
    }
}