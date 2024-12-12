using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class MaterialIndexModel
    {
        public MaterialSearch Search { get; set; }
        public PagedResult<Material> Data { get; set; }
    }
}
