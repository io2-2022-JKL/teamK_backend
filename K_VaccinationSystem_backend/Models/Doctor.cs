namespace K_VaccinationSystem_backend.Models
{
    public class Doctor : User
    {
        public VaccinationCenter VaccinationCenter_ { get; set; }

        //vacc archive
        //future vacc:
        ICollection<Appointment> Appointments { get; set; }
        public Patient PatientAccount { get; set; }
        public bool Active { get; set; }
    }
}
