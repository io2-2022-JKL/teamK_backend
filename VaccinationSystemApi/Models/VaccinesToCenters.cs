using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Models
{
    public class VaccinesToCenters
    {
        public Guid Id { get; set; }
        public Vaccine Vaccine_ { get; set; }
        public VaccinationCenter VaccinationCenter_ { get; set; }
    }
}
