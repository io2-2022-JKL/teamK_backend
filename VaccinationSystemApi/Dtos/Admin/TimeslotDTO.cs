using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class TimeslotDTO
    {
        public Guid id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public bool isFree { get; set; }
        public bool active { get; set; }
    }
}
