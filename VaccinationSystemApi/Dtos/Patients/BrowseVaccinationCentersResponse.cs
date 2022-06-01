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
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public ICollection<Vaccine> AvailableVaccines { get; init; }

        public TimeHours[] OpeningHours { get; init; }
        public TimeHours[] ClosingHours { get; init; }
        public ICollection<Doctor> Doctors { get; init; }
        public bool Active { get; init; }
    }
}
