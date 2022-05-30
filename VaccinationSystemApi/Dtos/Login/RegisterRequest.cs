using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Login
{
    public class RegisterRequest
    {

        /*"PESEL": 95050206951,
        "firstName": "Anna Maria",
        "lastName": "Wesołowska",
        "mail": "AMW@gmail.com",
        "dateOfBirth": "03-03-2000",
        "password": "AnnaWesołowskaMaria",
        "phoneNumber": 123234345*/

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
