using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class EditDoctorRequest
    {
        public Guid doctorId { get; set; }
        public string PESEL { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
        public string dateOfBirth { get; set; }
        public string phoneNumber { get; set; }
        public bool active { get; set; }
        public Guid vaccinationCenterId { get; set; }
    }
}
