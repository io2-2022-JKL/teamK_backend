using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record CreateTimeSlotRequest
    {
        public DateTime From { get; init; }
        public DateTime To { get; init; }
        public int Duration { get; init; }
    }
}
