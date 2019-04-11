using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Spinning.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TimeslotId { get; set; }
        public virtual SpinningUser SpinningUsers { get; set; }
        public virtual Timeslot Timeslot { get; set; }
    }
}
