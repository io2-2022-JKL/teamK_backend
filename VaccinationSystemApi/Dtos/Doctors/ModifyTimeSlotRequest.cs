using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record ModifyTimeSlotRequest
    {
        public string From { get; init; }
        public string To { get; init; }
    }
}
