using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _uof;
        public ClientService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Client> Get(int? Id)
        {
            return await _uof.ClientRepository.Get(Id);
        }

       
        public async Task<bool> Includes(int Id)
        {
            return await _uof.ClientRepository.Includes(Id);
        }

        public Task<PagedResult<Client>> List(int page, int pageSize)
        {
            return _uof.ClientRepository.List(page, pageSize);
        }

        public async Task Save(Client item)
        {
            await _uof.ClientRepository.Save(item);
        }
        public async Task Delete(int Id)
        {
            await _uof.ClientRepository.Delete(Id);

        }
    }
}