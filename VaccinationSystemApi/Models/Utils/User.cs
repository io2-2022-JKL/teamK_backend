using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Models.Utils
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Pesel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EMail { get; set; }
        public string? Password { get; set; } //legacy argument, always null
        public string PhoneNumber { get; set; }
    }
}
