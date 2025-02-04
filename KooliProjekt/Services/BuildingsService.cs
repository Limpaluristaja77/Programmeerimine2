using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class BuildingsService : IBuidlingsService
    {
        private readonly IUnitOfWork _uof;
        public BuildingsService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Buildings> Get(int? Id)
        {
            return await _uof.BuildingsRepository.Get(Id);
        }

        public async Task<DbSet<Panel>> GetPanelsAsync()
        {
            return await _uof.BuildingsRepository.GetAllPanels();
        }

        public async Task<DbSet<Material>> GetMaterialsAsync()
        {
            return await _uof.BuildingsRepository.GetAllMaterials();
        }
        public async Task<bool> Includes(int Id)
        {
            return await _uof.BuildingsRepository.Includes(Id);
        }

        public Task<PagedResult<Buildings>> List(int page, int pageSize)
        {
            return _uof.BuildingsRepository.List(page, pageSize);
        }

        public async Task Save(Buildings item)
        {
            await _uof.BuildingsRepository.Save(item);
        }
        public async Task Delete(int Id)
        {
            await _uof.BuildingsRepository.Delete(Id);

        }
    }
}