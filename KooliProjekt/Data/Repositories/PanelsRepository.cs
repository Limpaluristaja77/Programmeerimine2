namespace KooliProjekt.Data.Repositories
{
    public class PanelsRepository : BaseRepository<Panel>, IPanelsRepository
    {
        public PanelsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Panel> Get(int id)
        {
            return await DbContext.Panels.FindAsync(id);

        }
    }
}
