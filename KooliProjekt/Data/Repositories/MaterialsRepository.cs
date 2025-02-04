using Microsoft.EntityFrameworkCore;
using static KooliProjekt.Data.Repositories.BuildingsRepository;

namespace KooliProjekt.Data.Repositories
{
    public class MaterialsRepository : BaseRepository<Material>, IMaterialsRepository
    {
        public MaterialsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Material> Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
