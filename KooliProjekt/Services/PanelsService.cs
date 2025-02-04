using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class PanelsService : IPanelsService
    {
        private readonly IUnitOfWork _uof;
        public PanelsService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Panel> Get(int? Id)
        {
            return await _uof.PanelsRepository.Get(Id);
        }


        public async Task<bool> Includes(int Id)
        {
            return await _uof.PanelsRepository.Includes(Id);
        }

        public Task<PagedResult<Panel>> List(int page, int pageSize)
        {
            return _uof.PanelsRepository.List(page, pageSize);
        }

        public async Task Save(Panel item)
        {
            await _uof.PanelsRepository.Save(item);
        }
        public async Task Delete(int Id)
        {
            await _uof.PanelsRepository.Delete(Id);

        }
    }
}