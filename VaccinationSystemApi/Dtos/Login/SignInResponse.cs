namespace VaccinationSystemApi.Dtos.Login
{
    public class SignInResponse
    {
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string Jwt { get; set; }
    }
}
