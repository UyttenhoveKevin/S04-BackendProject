using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Spinning.Models
{
    [Table("Rooms")]
    public class Room
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }

        [Range(0, 50, ErrorMessage = "Number of bikes must be between 0 and 50")]
        [DisplayName("Number of Bikes")]
        [Required(ErrorMessage = "BikeCount field can't be empty")]
        public int BikeCount { get; set; }

        [Range(0, 500,ErrorMessage = "Room number must be between 0 and 500")]
        [DisplayName("Room number")]
        [Required(ErrorMessage = "Room number field can't be empty")]
        public int RoomNr { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; }

        public Room()
        {
            Timeslots = new HashSet<Timeslot>();
        }
    }
}
