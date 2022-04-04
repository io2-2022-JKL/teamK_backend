using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public class FilterTimeslotsResponse
    {
        public string TimeSlotId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string VaccinationCenterName { get; set; }
        public string VaccinationCenterCity { get; set; }
        public string VaccinationCenterStreet { get; set; }
        public ICollection<VaccineDTO> AvailableVaccines { get; set; }
        public ICollection<OpeningHoursDTO> openingHours { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
    }
}
