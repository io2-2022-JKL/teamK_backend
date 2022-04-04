using Xunit;

namespace Vaccination.Tests;

public class PatientTest
{
    VaccinationSystemApi.Controllers.PatientController patientController = 
        new VaccinationSystemApi.Controllers.PatientController(new VaccinationSystemApi.Repositories.VaccinationSystemRepository());
    
    [Fact]
    public void HoursFromTimeHours1()
    {
        var timeHours = new VaccinationSystemApi.Models.Utils.TimeHours(4, 34);

        string result = patientController.HourStringFromTimeHours(timeHours);
        Assert.Equal("4:34", result);
    }
}