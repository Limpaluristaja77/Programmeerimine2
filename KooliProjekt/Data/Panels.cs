﻿using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Panel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal UnitCost { get; set; }
        [Required]
        [StringLength (255)]
        public string Manufacturer { get; set; }



    }
}
