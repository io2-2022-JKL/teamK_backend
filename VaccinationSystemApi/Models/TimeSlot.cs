using System;

namespace VaccinationSystemApi.Models
{
    public class TimeSlot
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid AssignedDoctorId { get; set; }
        public Doctor AssignedDoctor { get; set; }
        public bool IsFree { get; set; }
        public bool Active { get; set; }
    }
}
