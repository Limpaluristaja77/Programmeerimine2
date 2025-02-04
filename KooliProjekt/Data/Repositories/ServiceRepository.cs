using Microsoft.EntityFrameworkCore;
using static KooliProjekt.Data.Repositories.BuildingsRepository;

namespace KooliProjekt.Data.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Service> Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
