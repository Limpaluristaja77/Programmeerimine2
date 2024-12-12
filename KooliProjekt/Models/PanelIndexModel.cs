using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class PanelIndexModel
    {

        public PanelSearch Search { get; set; }
        public PagedResult<Panel> Data { get; set; }
    }
}
