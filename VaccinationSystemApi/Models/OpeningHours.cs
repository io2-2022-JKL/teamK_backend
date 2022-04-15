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
    }
}
