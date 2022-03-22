using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Repositories.Interfaces
{
    public interface IVaccinationSystemRepository
    {
        IEnumerable<Patient> GetPatients();
        Patient GetPatient(Guid id);

        IEnumerable<VaccinationCenter> GetCenters();
        VaccinationCenter GetCenter(Guid id);
        IEnumerable<Doctor> GetDoctors();
        Doctor GetDoctor(Guid id);
        VaccinationCenter GetCenterOfDoctor(Guid doctorId);
        IEnumerable<TimeSlot> GetTimeSlots();
        Guid CreateAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId);
    }
}
