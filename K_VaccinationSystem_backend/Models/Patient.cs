namespace K_VaccinationSystem_backend.Models
{
    public class Patient : User
    {
        //vacc count
        //should it be a field in a db? 

        //vacc history
        //future vacc:
        public ICollection<Appointment> Appointments { get; set; }
        
        //certificates:
        public ICollection<Certificate> Certificates { get; set; }
        public bool Active { get; set; }
    }
}
