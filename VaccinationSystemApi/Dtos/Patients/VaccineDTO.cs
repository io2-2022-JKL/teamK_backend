namespace VaccinationSystemApi.Dtos.Patients
{
    public class VaccineDTO
    {
        public string VaccineId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public int NumberOfDoses { get; set; }
        public int MinDaysBetweenDoses { get; set; }
        public int MaxDaysBetweenDoses { get; set; }
        public string Virus { get; set; }
        public int MinPatientAge { get; set; }
        public int MaxPatientAge { get; set; }
    }
}
