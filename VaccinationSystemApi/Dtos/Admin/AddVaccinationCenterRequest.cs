using System;
using System.Collections.Generic;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class AddVaccinationCenterRequest
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<Guid> VaccineIds { get; set; }
        public ICollection<OpeningHoursAdminDTO> OpeningHoursDays { get; set; }
        public bool Active { get; set; }
    }
}
