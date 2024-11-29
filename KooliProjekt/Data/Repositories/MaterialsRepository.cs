using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class MaterialsRepository : BaseRepository<Material>, IMaterialsRepository
{
    public MaterialsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Material> Get(int id)
    {
        return await DbContext.Materials.FindAsync(id);

    }
}
}
