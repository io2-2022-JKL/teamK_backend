using System;
using Xunit;
using System.Collections.Generic;
using VaccinationSystemApi.Controllers;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;
using VaccinationSystemApi.Dtos.Patients;
using VaccinationSystemApi.Helpers;
using System.Linq;
using Xunit;
using Newtonsoft.Json;

namespace VaccinationSystem.TestsNet5
{
    public class TimeslotValidatorTests
    {
        public static readonly object[][] Availbility1Data =
        {
            new object[] {  new DateTime(2023, 03, 02, 8, 0, 0), new DateTime(2023, 03, 02, 9, 0, 0),
                            new DateTime(2023, 03, 02, 8, 30, 0), new DateTime(2023, 03, 02, 8, 45, 0), false},
        };

        [Theory, MemberData(nameof(Availbility1Data))]
        public void Availability1(DateTime startSlot, DateTime endSlot, DateTime startDate, DateTime endDate, bool result)
        {
            Assert.True(TimeSlotValidator.IsAvailable(startSlot, endSlot, startDate, endDate) == result);
        }

        public static readonly object[][] Validation1Data =
        {
            new object[] {  new DateTime(2023, 03, 02, 8, 0, 0), new DateTime(2023, 03, 02, 7, 45, 0), false},
            new object[] {  new DateTime(2023, 03, 02, 7, 0, 0), new DateTime(2023, 03, 02, 7, 45, 0), true},
        };
        [Theory, MemberData(nameof(Validation1Data))]
        public void Validation1(DateTime startDate, DateTime endDate, bool result)
        {
            var exception = Record.Exception(() => TimeSlotValidator.Validate(startDate, endDate));
            if (!result)
                Assert.NotNull(exception);
            else
                Assert.Null(exception);
        }

    }
}