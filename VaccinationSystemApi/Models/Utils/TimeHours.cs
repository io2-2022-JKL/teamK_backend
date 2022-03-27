using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Models.Utils
{
    public class TimeHours
    {
        public Guid Id { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }

        public TimeHours(int hour, int minutes = 0)
        {
            Hour = hour;
            Minutes = minutes;
        }
    }
}
