using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Budget : Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        [StringLength(50)]
        public int BuildingsId { get; set; }
        [Required]
        [StringLength(50)]
        public int ServicesId { get; set; }
        [Required]
        [StringLength(50)]
        public DateTime Date { get; set; }
        [Required]
        public decimal Cost { get; set; }


        public Client Client { get; set; }
        public Buildings Buildings { get; set; }
        public Service Services { get; set; }
        


       

            

    }

}
