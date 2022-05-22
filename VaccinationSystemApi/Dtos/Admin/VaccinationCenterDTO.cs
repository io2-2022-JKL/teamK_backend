using System;
using System.Collections.Generic;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class VaccinationCenterDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<VaccineDTO> Vaccines { get; set; }
        public ICollection<OpeningHoursDTO> OpeningHoursDays { get; set; }
        public bool Active { get; set; }
    }
}
