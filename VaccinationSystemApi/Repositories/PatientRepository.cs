using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Repositories.Interfaces;

namespace VaccinationSystemApi.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly List<Patient> patients = new()
        {
            new Patient
            {
                Active = true,
                Appointments = null,
                Certificates = null,
                DateOfBirth = new DateTime(2000, 11, 8),
                EMail = "patient1@mymail.com",
                FirstName = "Patient1",
                LastName = "LeSurname",
                Id = Guid.NewGuid(),
                Password = "password",
                Pesel = "001131451142",
                PhoneNumber = "661541786"
            },
            new Patient
            {
                Active = true,
                Appointments = null,
                Certificates = null,
                DateOfBirth = new DateTime(1972, 11, 8),
                EMail = "hassterplan@mymail.com",
                FirstName = "Gunther",
                LastName = "Steiner",
                Id = Guid.NewGuid(),
                Password = "doors",
                Pesel = "72110892412",
                PhoneNumber = ""
            },

        };

        public Patient GetPatient(Guid id)
        {
            return patients.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Patient> GetPatients()
        {
            return patients;
        }
    }
}
