using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public class FilterTimeslotsResponse
    {
        public string timeSlotId { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string vaccinationCenterName { get; set; }
        public string vaccinationCenterCity { get; set; }
        public string vaccinationCenterStreet { get; set; }
        public ICollection<VaccineDTO> availableVaccines { get; set; }
        public ICollection<OpeningHoursDTO> openingHours { get; set; }
        public string doctorFirstName { get; set; }
        public string doctorLastName { get; set; }
    }
}
