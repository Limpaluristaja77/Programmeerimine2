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
        public int PanelId { get; set; }
        [Required]
        public int MaterialId { get; set; }


        public Panels Panels { get; set; }
        public Materials Materials { get; set; }

    } 
}
