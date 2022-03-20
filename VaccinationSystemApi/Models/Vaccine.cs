using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Models
{
    public class Vaccine
    {
        public Guid Id { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public int NumberOfDoses { get; set; }
        public int MinDaysBetweenDoses { get; set; }
        public int MaxDaysBetweenDoses { get; set; }
        public Guid VirusId { get; set; }
        public int MinPatientAge { get; set; }
        public int MaxPatientAge { get; set; }
        public bool Used { get; set; }
    }
}
