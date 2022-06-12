using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record BrowseVaccinationCentersResponse
    {
        public Guid id { get; init; }
        public string name { get; init; }
        public string city { get; init; }
        public string address { get; init; }
        public ICollection<Vaccine> availableVaccines { get; init; }
        public TimeHours[] openingHours { get; init; }
        public TimeHours[] closingHours { get; init; }
        public ICollection<Doctor> doctors { get; init; }
        public bool active { get; init; }
    }
}
