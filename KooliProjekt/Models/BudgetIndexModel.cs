using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class BudgetIndexModel
    {
        public BudgetSearch Search { get; set; }
        public PagedResult<Budget> Data { get; set; }
    }
}