namespace KooliProjekt.Data.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Client> Get(int id)
        {
            return await DbContext.Clients.FindAsync(id);
        }
    }
}
