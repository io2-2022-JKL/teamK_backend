namespace K_VaccinationSystem_backend.DTOs.Patient.Response
{
    public class VisitDTO
    {
        public string VaccineType { get; set; }
        public string VaccineBatch { get; set; }
        public int WhichVaccineDose { get; set; }
        public string VisitId { get; set; }
        public DateTime WindowBegin { get; set; }
        public DateTime WindowEnd { get; set; }
        public string VaccinationCenterName { get; set; }
        public string VaccinationCenterCity { get; set; }
        public string VaccinationCenterStreet { get; set; }
    }
}
