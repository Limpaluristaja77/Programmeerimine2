using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }


    }
}
