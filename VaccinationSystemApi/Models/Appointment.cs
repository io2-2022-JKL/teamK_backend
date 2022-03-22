using System;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public int WhichDose { get; set; }
        public Guid TimeSlotId { get; set; }
        public Guid PatientId { get; set; }
        public Guid VaccineId { get; set; }
        public AppointmentStatus Status { get; set; }
        public string VaccineBatchNumber { get; set; }
    }
}
