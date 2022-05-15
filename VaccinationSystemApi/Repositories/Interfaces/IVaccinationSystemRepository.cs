using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Dtos.Login;
using VaccinationSystemApi.Dtos.Admin;

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
        IEnumerable<Certificate> GetPatientCertificates(Guid patientId);
        Guid CreateAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId);
        Appointment GetAppointment(Guid id);
        void CancelAppointment(Guid id);
        void CreateTimeSlot(TimeSlot timeSlot);
        Doctor GetDoctorByTimeSlot(Guid id);
        void DeleteTimeSlot(Guid id);
        void ModifyTimeSlot(Guid timeSlotId, DateTime from, DateTime to);
        public IEnumerable<TimeSlot> GetDoctorActiveSlots(Guid doctorId, string date);
        public IEnumerable<Appointment> GetIncomingAppointments(Guid patientId);
        public IEnumerable<Appointment> GetFormerAppointments(Guid patientId);
        public IEnumerable<TimeSlot> FilterTimeslots(string city, DateTime dateFrom, DateTime dateTo, string virus);
        bool CreatePatient(RegisterRequest registerRequest, Guid guid = default(Guid));
        bool EditPatient(PatientDTO patientToEdit, out bool wasPatientFound);
        bool RemovePatient(Guid patientId);
        IEnumerable<Doctor> GetDoctorsWithMatchingVaccinationCentres();
        bool EditDoctor(DoctorDTO doctorData, out bool doctorFound);
        bool AddDoctor(Doctor doctorToAdd);
        bool DeleteDoctor(Guid DoctorId, out bool doctorFound);
    }
}
