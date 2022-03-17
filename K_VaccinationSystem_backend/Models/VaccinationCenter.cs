namespace K_VaccinationSystem_backend.Models
{
    public class VaccinationCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  City { get; set; }
        public string Address { get; set; }
        //available vaccines:
        public ICollection<Vaccine> AvailableVaccines { get; set;}

        public TimeOnly[] OpeningHours { get; set; }
        public TimeOnly[] ClosingHours { get; set; }
        //doctors:
        public ICollection<Doctor> Doctors { get; set; }

        public bool Active { get; set; }

    }
}
