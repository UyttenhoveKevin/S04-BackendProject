using Spinning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spinning.API.DTOs
{
    public class Reservation_DTO
    {     
        public int TimeSlotId { get; set; }
        public Timeslot_DTO Timeslot { get; set; }

        public string SpinningUserId { get; set; }
    }
}
