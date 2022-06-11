using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Admin
{
    public record AddVaccinationCenterResponse
    {
        public string name { get; init; }
        public string city { get; init; }
        public string street { get; init; }
        public ICollection<Guid> vaccineIds { get; init; }
        public ICollection<OpeningHoursAdminDTO> openingHoursDays { get; init; }
        public bool active { get; init; }

    }
}
