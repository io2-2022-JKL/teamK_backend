using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Login
{
    public class RegisterRequest
    {
        [Required]
        public string pesel { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string mail { get; set; }
        public string dateOfBirth { get; set; }
        [Required]
        public string password { get; set; }
        public string phoneNumber { get; set; }

    }   
}
