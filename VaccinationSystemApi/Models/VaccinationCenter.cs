using System;
using System.Collections.Generic;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Models
{
    public class VaccinationCenter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public ICollection<Vaccine> AvailableVaccines { get; set; }
        public OpeningHours OpeningHours_ { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public bool Active { get; set; }

    }
}
