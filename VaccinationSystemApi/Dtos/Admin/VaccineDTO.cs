using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class VaccineDTO
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Virus { get; set; }
    }
}
