namespace K_VaccinationSystem_backend.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Pesel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        }
}
