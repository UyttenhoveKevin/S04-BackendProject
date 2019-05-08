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
        public int TimeSlotId { get; set; }
        public string SpinningUserId { get; set; }
        public virtual SpinningUser SpinningUser { get; set; }
        public virtual Timeslot Timeslot { get; set; }
    }
}
