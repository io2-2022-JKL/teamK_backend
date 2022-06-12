using System;
using System.Collections.Generic;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class AddVaccinationCenterRequest
    {
        public string name { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public ICollection<Guid> vaccineIds { get; set; }
        public ICollection<OpeningHoursAdminDTO> openingHoursDays { get; set; }
        public bool active { get; set; }
    }
}
