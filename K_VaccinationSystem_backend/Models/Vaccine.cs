namespace K_VaccinationSystem_backend.Models
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public int NumberOfDoses { get; set; }
        public int MinDaysBetweenDoses { get; set; }
        public int MaxDaysBetweenDoses { get; set; }
        public Virus Virus_ { get; set; }
        public int MinPatientAge { get; set; }
        public int MaxPatientAge { get; set; }
        public bool Used { get; set; }
    }
}
