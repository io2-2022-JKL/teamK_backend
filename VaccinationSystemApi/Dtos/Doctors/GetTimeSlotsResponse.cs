using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetTimeSlotsResponse
    {
        public string id { get; init; }
        public string from { get; init; }
        public string to { get; init; }
        public bool isFree { get; init; }
    }
}
