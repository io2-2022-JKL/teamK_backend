using System;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public int WhichDose { get; set; }
        public Guid TimeslotId { get; set; }
        public TimeSlot TimeSlot_ { get; set; }
        public Patient Patient_ { get; set; }
        //public Guid PatientId { get; set; }
        public Vaccine Vaccine_ { get; set; }
        public AppointmentStatus Status { get; set; }
        public string VaccineBatchNumber { get; set; }
    }
}
