using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record CreateTimeSlotRequest
    {
        public string windowBegin { get; init; }
        public string windowEnd { get; init; }
        public int timeSlotDurationInMinutes { get; init; }
    }
}
