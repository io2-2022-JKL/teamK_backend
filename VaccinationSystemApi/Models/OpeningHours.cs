using System;
using System.Collections.Generic;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Models
{
    public class OpeningHours
    {
        public Guid Id { get; set; }
        public Guid VaccCenterId { get; set; }
        public VaccinationCenter VaccCenter { get; set; }
        public TimeHours MondayOpen { get; set; }
        public TimeHours MondayClose { get; set; }
        public TimeHours TuesdayOpen { get; set; }
        public TimeHours TuesdayClose { get; set; }
        public TimeHours WednesdayOpen { get; set; }
        public TimeHours WednesdayClose { get; set; }
        public TimeHours ThursdayOpen { get; set; }
        public TimeHours ThursdayClose { get; set; }
        public TimeHours FridayOpen { get; set; }
        public TimeHours FridayClose { get; set; }
        public TimeHours SaturdayOpen { get; set; }
        public TimeHours SaturdayClose { get; set; }
        public TimeHours SundayOpen { get; set; }
        public TimeHours SundayClose { get; set; }
    }
}
