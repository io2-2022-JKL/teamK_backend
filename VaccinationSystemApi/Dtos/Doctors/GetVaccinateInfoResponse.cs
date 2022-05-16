using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetVaccinateInfoResponse
    {
        public string vaccineName { get; init; }
        public string vaccineCompany { get; init; }
        public int numberOfDoses { get; init; }
        public int minDaysBetweenDoses { get; init; }
        public int maxDaysBetweenDoses { get; init; }
        public string virusName { get; init; }
        public int minPatientAge { get; init; }
        public int maxPatientAge { get; init; }
        public string patientFirstName { get; init; }
        public string patientLastName { get; init; }
        public string pesel { get; init; }
        public string dateOfBirth { get; init; }
        public string from { get; init; }
        public string to { get; init; }

    }
}
