﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public class AppointmentResponse
    {
        public string VaccineName { get; set; }
        public string VaccineCompany { get; set; }
        public string VaccineVirus { get; set; }
        public int  WhichVaccineDose { get; set; }
        public string AppointmentId { get; set; }
        public DateTime WindowBegin { get; set; }
        public DateTime WindowEnd { get; set; }
        public string VaccinationCenterName { get; set; }
        public string VaccinationCenterCity { get; set; }
        public string VaccinationCenterStreet { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }

    }
}
