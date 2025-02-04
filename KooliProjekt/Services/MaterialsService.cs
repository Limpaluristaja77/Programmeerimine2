using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IUnitOfWork _uof;
        public MaterialsService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Material> Get(int? Id)
        {
            return await _uof.MaterialsRepository.Get(Id);
        }


        public async Task<bool> Includes(int Id)
        {
            return await _uof.MaterialsRepository.Includes(Id);
        }

        public Task<PagedResult<Material>> List(int page, int pageSize)
        {
            return _uof.MaterialsRepository.List(page, pageSize);
        }

        public async Task Save(Material item)
        {
            await _uof.MaterialsRepository.Save(item);
        }
        public async Task Delete(int Id)
        {
            await _uof.MaterialsRepository.Delete(Id);

        }
    }
}