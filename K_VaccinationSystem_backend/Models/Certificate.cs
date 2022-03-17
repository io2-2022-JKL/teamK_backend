namespace K_VaccinationSystem_backend.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Url { get; set; }

        //this wasnt in documentation but is necessary:
        public Patient Patient_ { get; set; }
    }
}
