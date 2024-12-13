using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class BuildingIndexModel
    {
        public BuildingSearch Search { get; set; }
        public PagedResult<Buildings> Data { get; set; }
    }
}
