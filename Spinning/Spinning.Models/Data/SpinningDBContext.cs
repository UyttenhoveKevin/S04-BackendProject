using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spinning.Models.Data
{
    public class SpinningDBContext:IdentityDbContext<SpinningUser>
    {
        public SpinningDBContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<SpinningUser> SpinningUsers { get; set; }
        public virtual DbSet<Timeslot> Timeslots { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        
    }
}
