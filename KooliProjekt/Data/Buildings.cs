using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Buildings
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string PanelId { get; set; }
        [Required]
        public string MaterialId { get; set; }


        public IList<Panel> Panels { get; set; }
        public IList<Material> Materials { get; set; }

        public Buildings()
        {
            Panels = new List<Panel>();
            Materials = new List<Material>();
        }

    } 
}
