using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetFormerAppointmentsResponse
    {
        public string VaccineName { get; init; }
        public string VaccineCompany { get; init; }
        public int VaccineDose { get; init; }
        public string VaccineVirus { get; init; }
        public string AppointmentId { get; init; }
        public string PatientFirstName { get; init; }
        public string PatientLastName { get; init; }
        public string Pesel { get; init; }
        public string State { get; init; }
        public string BatchNumber { get; init; }
        public string From { get; init; }
        public string To { get; init; }
        public string CertifyState { get; init; }
    }
}
