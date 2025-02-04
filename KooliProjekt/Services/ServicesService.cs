using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IUnitOfWork _uof;
        public ServicesService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Service> Get(int? Id)
        {
            return await _uof.ServiceRepository.Get(Id);
        }


        public async Task<bool> Includes(int Id)
        {
            return await _uof.ServiceRepository.Includes(Id);
        }

        public Task<PagedResult<Service>> List(int page, int pageSize)
        {
            return _uof.ServiceRepository.List(page, pageSize);
        }

        public async Task Save(Service item)
        {
            await _uof.ServiceRepository.Save(item);
        }
        public async Task Delete(int Id)
        {
            await _uof.ServiceRepository.Delete(Id);

        }
    }
}