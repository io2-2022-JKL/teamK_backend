using System.Collections.Generic;
using VaccinationSystemApi.Dtos.Patients;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Services
{
    public class TimeHoursService
    {
        public string HourStringFromTimeHours(Models.Utils.TimeHours timeHours)
        {
            string hour = timeHours.Hour < 10 ? '0' + timeHours.Hour.ToString() : timeHours.Hour.ToString();
            string minute = timeHours.Minutes < 10 ? '0' + timeHours.Minutes.ToString() : timeHours.Minutes.ToString();
            return hour + ":" + minute;
        }

        public ICollection<OpeningHoursDTO> OpeningHoursToDTO(OpeningHours openingHours)
        {
            var mondayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.MondayOpen),
                To = HourStringFromTimeHours(openingHours.MondayClose),
            };
            var tuesdayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.TuesdayOpen),
                To = HourStringFromTimeHours(openingHours.TuesdayClose),
            };
            var wednesdayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.WednesdayOpen),
                To = HourStringFromTimeHours(openingHours.WednesdayClose),
            };
            var thursdayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.ThursdayOpen),
                To = HourStringFromTimeHours(openingHours.ThursdayClose),
            };
            var fridayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.FridayOpen),
                To = HourStringFromTimeHours(openingHours.FridayClose),
            };
            var saturdayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.SaturdayOpen),
                To = HourStringFromTimeHours(openingHours.SaturdayClose),
            };
            var sundayDto = new OpeningHoursDTO()
            {
                From = HourStringFromTimeHours(openingHours.SundayOpen),
                To = HourStringFromTimeHours(openingHours.SundayClose),
            };

            return new List<OpeningHoursDTO>()
            {
                mondayDto, tuesdayDto, wednesdayDto, thursdayDto, fridayDto, saturdayDto, sundayDto
            };
        }
    }
}
