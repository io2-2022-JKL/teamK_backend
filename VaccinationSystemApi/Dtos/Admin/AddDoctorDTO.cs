using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class AddDoctorDTO
    {
        public Guid PatientId { get; set; }
        public Guid VaccinationCenterId { get; set; }
    }
}
