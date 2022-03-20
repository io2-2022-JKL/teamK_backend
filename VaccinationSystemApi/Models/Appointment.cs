using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public int WhichDose { get; set; }
        public Guid TimeSlotId { get; set; }
        public Guid PatientId { get; set; }
        public Guid VaccineId { get; set; }
        public bool Completed { get; set; }
        public string VaccineBatchNumber { get; set; }
    }
}
