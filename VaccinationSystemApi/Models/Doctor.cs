using System;
using System.Collections.Generic;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Models
{
    public class Doctor : User
    {
        public Guid VaccinationCenterId { get; set; }
        public VaccinationCenter VaccinationCenter_ { get; set; }
        ICollection<Appointment> Appointments { get; set; }
        public Guid? PatientAccountId { get; set; }
        public Patient PatientAccount { get; set; }
        public bool Active { get; set; }
    }
}
