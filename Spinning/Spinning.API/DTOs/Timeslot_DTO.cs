using Spinning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spinning.API.DTOs
{
    public class Timeslot_DTO
    {
        //public int Id { get; set; }
        //public int RoomId { get; set; }

        public DateTime Date { get; set; }
        //public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual Room_DTO Room { get; set; }
    }
}
