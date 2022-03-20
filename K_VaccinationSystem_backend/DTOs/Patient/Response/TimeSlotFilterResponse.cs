namespace K_VaccinationSystem_backend.DTOs.Patient.Response
{
    public class TimeSlotFilterResponse
    {
        public string DayId { get; set; }
        public DateOnly Day { get; set; }
        public string VaccinationCenterId { get; set; }
        public string VaccinationCenterName { get; set; }
        public string VaccinationCenterCity { get; set; }
        public string VaccinationCenterStreet { get; set; }
    }
}
