using Microsoft.EntityFrameworkCore;
using static KooliProjekt.Data.Repositories.BuildingsRepository;

namespace KooliProjekt.Data.Repositories
{
    public class PanelsRepository : BaseRepository<Panel>, IPanelsRepository
    {
        public PanelsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Panel> Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
