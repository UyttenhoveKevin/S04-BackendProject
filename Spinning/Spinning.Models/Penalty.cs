using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Spinning.Models
{
    [Table("Penalties")]
    public class Penalty
    {               
        public int Id { get; set; }
        public string SpinningUserId { get; set; }      
        [Required(ErrorMessage = "StartDate field can't be empty")]
        public DateTime StartdatePenalty { get; set; }
        [Required(ErrorMessage = "EndDate field can't be empty")]
        public DateTime EnddatePenalty { get; set; }

        public virtual SpinningUser SpinningUser { get; set; }
    }
}
