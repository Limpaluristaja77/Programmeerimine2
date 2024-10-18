using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Budget
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int BuildingsId { get; set; }
        [Required]
        public int ServicesId { get; set; }
        [Required]
        [StringLength(10)]
        public DateTime Date { get; set; }
        [Required]
        public decimal Cost { get; set; }


        public Client Client { get; set; }
        public Buildings Buildings { get; set; }
        public Services Services { get; set; }
        


       

            

    }

}
