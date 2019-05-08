using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spinning.API.DTOs
{
    public class Room_DTO
    {
        //public int Id { get; set; }    
        public int BikeCount { get; set; }
        public int RoomNr { get; set; }

        //public virtual ICollection<Timeslot_DTO> Timeslots { get; set; }
    }
}
