using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetIncomingAppointmentsResponse
    {
        public string vaccineName { get; init; }
        public string vaccineCompany { get; init; }
        public int whichVaccineDose { get; init; }
        public string vaccineVirus { get; init; }
        public string appointmentId { get; init; }
        public string patientFirstName { get; init; }
        public string patientLastName { get; init; }
        public string from { get; init; }
        public string to { get; init; }
    }
}
