using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class TimeslotDTO
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsFree { get; set; }
        public bool Active { get; set; }
    }
}
