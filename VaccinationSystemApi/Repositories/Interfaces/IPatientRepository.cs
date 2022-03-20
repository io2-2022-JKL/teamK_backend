using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(Guid id);
        //ICollection<VaccinationCenter> BrowseVaccinacionCenters(string city);
    }
}
