using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Buildings : Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        
        public string Name { get; set; }
        [Required]
        public int PanelId { get; set; }
        [Required]
        public int MaterialId { get; set; }


        public IList<Panel> Panels { get; set; }
        public IList<Material> Materials { get; set; }

        public Buildings()
        {
            Panels = new List<Panel>();
            Materials = new List<Material>();
        }

    } 
}
