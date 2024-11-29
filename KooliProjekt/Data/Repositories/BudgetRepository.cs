using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class BudgetRepository : BaseRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Budget> Get(int id)
        {
            return await DbContext.Budgets
                .Include(list => list.BuildingsId)
                .Include(list => list.ClientId)
                .Include(list => list.ServicesId)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();


        }
    }
}
