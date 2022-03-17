namespace K_VaccinationSystem_backend.Models
{
    public class Appointment
    {
        public int WhichDose { get; set; }
        public TimeSlot TimeSlot_ { get; set; }
        public Patient Patient_ { get; set; }
        public Vaccine Vaccine_ { get; set; }
        public bool Completed { get; set; }
        public string VaccineBatchNumber { get; set; }
    }
}
