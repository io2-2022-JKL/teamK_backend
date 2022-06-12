using System;
using System.Collections.Generic;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class VaccinationCenterAdminDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public ICollection<VaccineAdminDTO> vaccines { get; set; }
        public ICollection<OpeningHoursAdminDTO> openingHoursDays { get; set; }
        public bool active { get; set; }
    }
}
