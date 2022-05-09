using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record BrowsePatientByIdResponse
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Pesel { get; init; }
        public DateTime DataOfBirth { get; init; }
        public string EMail { get; init; }
        public string PhoneNumber { get; init; }
    }
}
