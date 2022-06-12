using System;

namespace VaccinationSystemApi.Dtos.Admin
{
    public class VaccineAdminDTO
    {
        public Guid id{ get; set; }
        public string name { get; set; }
        public string companyName { get; set; }
        public string virus { get; set; }
    }
}
