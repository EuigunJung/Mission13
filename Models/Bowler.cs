using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public class Bowler
    {
        [Key]
        [Required]
        public int BowlerID { get; set; }
        [Required]
        [MaxLength (15)]
        public string BowlerFirstName { get; set; }
        [MaxLength(2)]
        public string BowlerMiddleInit { get; set; }
        [Required]
        [MaxLength(15)]
        public string BowlerLastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string BowlerAddress { get; set; }
        [Required]
        [MaxLength(15)]
        public string BowlerCity { get; set; }
        [Required]
        [MaxLength(2)]
        public string BowlerState { get; set; }
        [Required]
        [MaxLength(5)]
        public string BowlerZip { get; set; }
        [Required]
        [MaxLength(15)]
        public string BowlerPhoneNumber { get; set;  }
        public int TeamID { get; set;  }
        public Team Team { get; set;  }
      
    }
}
