using Microsoft.AspNetCore.Mvc.Rendering;
using Spinning.Models;
using Spinning.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spinning.ViewModels
{
    public class TimeslotRoomViewModel
    {
        public int RoomNrs { get; set; }
        public Timeslot Timeslot { get; set; }


    }
}
