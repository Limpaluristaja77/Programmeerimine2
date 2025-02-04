using Microsoft.EntityFrameworkCore;
using static KooliProjekt.Data.Repositories.BudgetRepository;

namespace KooliProjekt.Data.Repositories
{
    public class BudgetRepository : BaseRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(ApplicationDbContext context) : base(context)
        {
        }

        Task IBaseRepository<Budget>.Delete(int? id)
        {
            throw new NotImplementedException();
        }

        Task<Budget> IBaseRepository<Budget>.Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
