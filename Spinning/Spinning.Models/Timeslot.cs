using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Spinning.Models
{
    [Table("Timeslots")]
    public class Timeslot
    {
        
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
