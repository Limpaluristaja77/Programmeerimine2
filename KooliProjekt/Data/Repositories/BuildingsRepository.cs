using Microsoft.EntityFrameworkCore;
using static KooliProjekt.Data.Repositories.BuildingsRepository;

namespace KooliProjekt.Data.Repositories
{
    public class BuildingsRepository : BaseRepository<Buildings>, IBuildingsRepository
    {
        public BuildingsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Buildings> Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
