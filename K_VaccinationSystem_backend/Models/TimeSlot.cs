namespace K_VaccinationSystem_backend.Models
{
    public class TimeSlot
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Doctor Doctor_ { get; set; }
        public bool IsFree { get; set; }
        public bool Active { get; set; }
    }
}
