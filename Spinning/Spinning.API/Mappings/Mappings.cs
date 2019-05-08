using AutoMapper;
using Spinning.API.DTOs;
using Spinning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spinning.API.Mappings
{
    public class Mappings : Profile
    {

        public Mappings()
        {
            CreateMap<Reservation, Reservation_DTO>().ReverseMap();
            CreateMap<Timeslot, Timeslot_DTO>().ReverseMap();
            CreateMap<Room, Room_DTO>().ReverseMap();
        }
    }
}
