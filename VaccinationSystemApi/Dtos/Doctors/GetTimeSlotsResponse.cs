using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetTimeSlotsResponse
    {
        public string Id { get; init; }
        public string From { get; init; }
        public string To { get; init; }
        public bool IsFree { get; init; }
    }
}
