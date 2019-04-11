using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spinning.Models
{
   
    public class SpinningUser:IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public SpinningUser():base()
        {
            Reservations = new HashSet<Reservation>();
        }
        
    }
}
