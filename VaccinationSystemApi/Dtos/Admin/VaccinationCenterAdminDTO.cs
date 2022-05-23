using System;
using System.Collections.Generic;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class VaccinationCenterAdminDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<VaccineAdminDTO> Vaccines { get; set; }
        public ICollection<OpeningHoursAdminDTO> OpeningHoursDays { get; set; }
        public bool Active { get; set; }
    }
}
