using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class BuildingsRepository : BaseRepository<Buildings>, IBuildingsRepository
    {
        public BuildingsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Buildings> Get(int id)
        {
            return await DbContext.Buildings
                .Include(list => list.MaterialId)
                .Include(list => list.PanelId)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();


        }
    }
}
