using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetFormerAppointmentsResponse
    {
        public string vaccineName { get; init; }
        public string vaccineCompany { get; init; }
        public int vaccineDose { get; init; }
        public string vaccineVirus { get; init; }
        public string appointmentId { get; init; }
        public string patientFirstName { get; init; }
        public string patientLastName { get; init; }
        public string PESEL { get; init; }
        public string state { get; init; }
        public string batchNumber { get; init; }
        public string from { get; init; }
        public string to { get; init; }
        public string certifyState { get; init; }
    }
}
