using Microsoft.EntityFrameworkCore;
using static KooliProjekt.Data.Repositories.BuildingsRepository;

namespace KooliProjekt.Data.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
