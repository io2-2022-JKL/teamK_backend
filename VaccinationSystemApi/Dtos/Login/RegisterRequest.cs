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
        public string PESEL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }   
}
