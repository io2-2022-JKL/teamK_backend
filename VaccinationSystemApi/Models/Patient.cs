using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Models
{
    public class Patient: User
    {
        public ICollection<Guid> Appointments { get; set; }
        public ICollection<Guid> Certificates { get; set; }
        public bool Active { get; set; }
    }
}
