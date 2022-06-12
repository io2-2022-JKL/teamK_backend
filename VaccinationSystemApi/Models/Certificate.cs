using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Models
{
    public class Certificate
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Patient Owner { get; set; }
        public Vaccine Vaccine_ { get; set; }
    }
}
