using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record TimeHoursResponse
    {
        public Guid Id { get; init; }
        public DateTime From { get; init; }
        public DateTime To { get; init; }
    }
}
