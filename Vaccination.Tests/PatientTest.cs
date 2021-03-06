using System.Collections.Generic;
using VaccinationSystemApi.Controllers;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;
using VaccinationSystemApi.Dtos.Patients;
using System.Linq;
using Xunit;
using Newtonsoft.Json;

namespace Vaccination.Tests;

public class PatientTest
{

    VaccinationSystemApi.Services.TimeHoursService timeHoursService = new VaccinationSystemApi.Services.TimeHoursService();
    
    [Fact]
    public void HoursFromTimeHours1()
    {
        var timeHours = new VaccinationSystemApi.Models.Utils.TimeHours(4, 34);

        string result = timeHoursService.HourStringFromTimeHours(timeHours);
        Assert.Equal("04:34", result);
    }
    [Fact]
    public void HoursCollectionFromOpeningHours1()
    {
        var timeHours = new OpeningHours()
        {
            MondayOpen = new TimeHours(3, 32),
            MondayClose = new TimeHours(18, 21),
            TuesdayOpen = new TimeHours(5, 22),
            TuesdayClose = new TimeHours(19, 7),
            WednesdayOpen = new TimeHours(4, 8),
            WednesdayClose = new TimeHours(21, 59),
            ThursdayOpen = new TimeHours(4, 9),
            ThursdayClose = new TimeHours(21, 33),
            FridayOpen = new TimeHours(15, 15),
            FridayClose = new TimeHours(15, 30),
            SaturdayOpen = new TimeHours(1, 0),
            SaturdayClose = new TimeHours(15, 0),
            SundayOpen = new TimeHours(2, 0),
            SundayClose = new TimeHours(22, 0),
        };

        List<OpeningHoursDTO> result = timeHoursService.OpeningHoursToDTO(timeHours).ToList();
        List<OpeningHoursDTO> should = new List<OpeningHoursDTO>(){
            new OpeningHoursDTO() {
                from = "03:32",
                to = "18:21"
            },
            new OpeningHoursDTO() {
                from = "05:22",
                to = "19:07"
            },
            new OpeningHoursDTO() {
                from = "04:08",
                to = "21:59"
            },
            new OpeningHoursDTO() {
                from = "04:09",
                to = "21:33"
            },
            new OpeningHoursDTO() {
                from = "15:15",
                to = "15:30"
            },
            new OpeningHoursDTO() {
                from = "01:00",
                to = "15:00"
            },
            new OpeningHoursDTO() {
                from = "02:00",
                to = "22:00"
            },
        };

        var obj1Str = JsonConvert.SerializeObject(should);
        var obj2Str = JsonConvert.SerializeObject(result);
        Assert.Equal(obj1Str, obj2Str);
    }
}