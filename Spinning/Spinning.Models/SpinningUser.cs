using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spinning.Models
{
    public class SpinningUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }

        public SpinningUser() : base()
        {
            Reservations = new HashSet<Reservation>();
            Penalties = new HashSet<Penalty>();
        }
    }
}