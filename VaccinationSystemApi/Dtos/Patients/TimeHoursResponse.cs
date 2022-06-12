using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record TimeHoursResponse
    {
        public Guid id { get; init; }
        public DateTime from { get; init; }
        public DateTime to { get; init; }
    }
}
