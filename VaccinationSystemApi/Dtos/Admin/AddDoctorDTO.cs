using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class AddDoctorDTO
    {
        public Guid patientId { get; set; }
        public Guid vaccinationCenterId { get; set; }
    }
}
