using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VaccinationSystemApi;
using FluentAssertions;
using VaccinationSystemApi.Dtos.Patients;

namespace VaccinationSystem.Tests.Integration.WebApi.Controllers
{
    [TestClass]

    public class PatientControllerTests
    {
        private HttpClient _client;

        public PatientControllerTests()
        {
            _client = new CustomWebApplicationFactory<Startup>().CreateClient();
        }

        [TestMethod]
        public async Task BrowseVaccinationCenters_ReturnsListOfCentersInWarsaw_WhenExisting()
        {
            //arrange
            var expectedNumberOfCenters = 1;

            //act
            var httpResponse = await _client.GetAsync("https://vaccinationsystemapi.azurewebsites.net/patient/centers/Warszawa");

            var json = await httpResponse.Content.ReadAsStringAsync();
            var vehicles = System.Text.Json.JsonSerializer.Deserialize<List<BrowseVaccinationCentersResponse>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });

            //assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            vehicles.Should().HaveCount(expectedNumberOfCenters);
        }
    }
}
