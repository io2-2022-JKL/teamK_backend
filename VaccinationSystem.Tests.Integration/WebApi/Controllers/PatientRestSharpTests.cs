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
using VaccinationSystem.Tests.Integration.WebApi;
namespace VaccinationSystem.TestsIntegration.WebApi.Controllers
{
    public class PatientRestSharpTests
    {
        private RestClient restClient;

        public PatientRestSharpTests()
        {
            string baseUri = "https://vaccinationsystemapi.azurewebsites.net/";
            var _client = new CustomWebApplicationFactory<Startup>().CreateClient();
            restClient = new RestClient(_client, new RestClientOptions() { BaseUrl = new System.Uri(baseUri) });
        }
        [Fact]
        public async void BrowseIncomingAppointments()
        {
            string patientId = "46F79416-2BDB-470A-9551-CD953BA061A2";
            var request = new RestRequest($"patient/appointments/incomingAppointments/{patientId}");
            AddHeadersToRequest(request);

            RestResponse registrationResponse = await restClient.GetAsync(request);

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
