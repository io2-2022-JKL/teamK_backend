namespace K_VaccinationSystem_backend.DTOs.Patient.Request
{
    public class TimeslotFilterRequest
    {
        public string VaccineType { get; set; }
        public string VaccineTypeCity { get; set; }
        public DateOnly VaccinationDayFrom { get; set; }
    }
}
