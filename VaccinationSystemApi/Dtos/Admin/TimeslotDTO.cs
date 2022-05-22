using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class TimeslotDTO
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsFree { get; set; }
        public bool Active { get; set; }
    }
}
