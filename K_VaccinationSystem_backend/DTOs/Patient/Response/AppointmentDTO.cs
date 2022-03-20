namespace K_VaccinationSystem_backend.DTOs.Patient.Response
{
    public class AppointmentDTO
    {
        public string VaccineType { get; set; }
        public int WhichVaccineDose { get; set; }
        public string VisitId { get; set; }
        public string WindowBegin { get; set; }
        public string WindowEnd { get; set; }
        public string VaccinationCenterName { get; set; }
        public string VaccinationCenterCity { get; set; }
        public string VaccinationCenterStreet { get; set; }
    }
}
