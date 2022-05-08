using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VaccinationSystemApi;
using FluentAssertions;
using Xunit;

using VaccinationSystemApi.Controllers;
using VaccinationSystemApi.Dtos.Patients;
using VaccinationSystemApi.Dtos.Login;

using Microsoft.Net.Http.Headers;
using RestSharp;

using System.Diagnostics;


namespace VaccinationSystem.TestsIntegration.WebApi.Controllers
{
 
    public class LoginControllerTests
    {
        private readonly RestClient _client;

        public LoginControllerTests()
        {
            string baseUri = "https://vaccinationsystemapi.azurewebsites.net/";
            _client = new RestClient(baseUri);
        }

        [Fact]
        public async void RegisteringUser()
        {
            var request = new RestRequest("register");

            request.AddJsonBody(new
            {
                DateOfBirth = new DateTime(2000, 5, 4),
                FirstName = "Senor",
                LastName = "Hans",
                Mail = "hans@gmail.com",
                Password = "123456",
                PESEL = "12345684501",
                PhoneNumber = "937528220"
            }
            );

            AddHeadersToRequest(request);

            RestResponse registrationResponse = await _client.PostAsync(request);

            Assert.True(registrationResponse.IsSuccessful);

        }
        private void AddHeadersToRequest(RestRequest request)
        {
            string tokenString = "0000000"; // temporary token string
            request.AddHeader("accept", "text/plain");
            request.AddHeader("Authorization", "Bearer " + tokenString);
            request.AddHeader("Content-Type", "application/json");
        }
    }
}
