using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spinning.Models
{
    public class Timeslot
    {
        [Required]
        public int Id { get; set; }
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Date field can't be empty")]
        public DateTime Date { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public Timeslot()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
