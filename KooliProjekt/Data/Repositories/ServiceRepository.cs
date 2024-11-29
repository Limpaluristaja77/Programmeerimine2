namespace KooliProjekt.Data.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Service> Get(int id)
        {
            return await DbContext.Services.FindAsync(id);

        }
    }
}
