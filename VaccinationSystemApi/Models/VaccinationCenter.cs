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
        //available vaccines:
        public ICollection<Vaccine> AvailableVaccines { get; set; }

        public TimeHours[] OpeningHours { get; set; }
        public TimeHours[] ClosingHours { get; set; }
        //doctors:
        public ICollection<Doctor> Doctors { get; set; }

        public bool Active { get; set; }

    }
}
