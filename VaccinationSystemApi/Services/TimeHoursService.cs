using System;
using System.Collections.Generic;
using System.Linq;
using VaccinationSystemApi.Dtos.Patients;
using VaccinationSystemApi.Dtos.Admin;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;

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

        public TimeHours TimeHoursFromHourString(string hourString)
        {
            string[] elements = hourString.Split(':');

            if (elements.Length != 2)
                throw new ArgumentException();

            int hour = int.Parse(elements[0]);
            int minute = int.Parse(elements[1]);

            return new TimeHours()
            {
                Id = Guid.NewGuid(),
                Hour = hour,
                Minutes = minute,
            };
        }

        public ICollection<OpeningHoursAdminDTO> DTOToAdminDTO(ICollection<OpeningHoursDTO> dtos)
        {
            var adminDtos = new List<OpeningHoursAdminDTO>();

            foreach(var dto in dtos)
            {
                var adminDto = new OpeningHoursAdminDTO()
                {
                    From = dto.From,
                    To = dto.To,
                };

                adminDtos.Add(adminDto);
            }

            return adminDtos;
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
            return null;
        }

        public OpeningHours DTOToOpeningHours(IList<VaccinationSystemApi.Dtos.Admin.OpeningHoursAdminDTO> hoursDtos)
        {
            if (hoursDtos.Count() != 7)
                throw new ArgumentException();

            var openingHours = new OpeningHours();
            openingHours.Id = Guid.NewGuid();
            openingHours.MondayOpen = TimeHoursFromHourString(hoursDtos[0].From);
            openingHours.MondayClose = TimeHoursFromHourString(hoursDtos[0].To);
            openingHours.TuesdayOpen = TimeHoursFromHourString(hoursDtos[1].From);
            openingHours.TuesdayClose = TimeHoursFromHourString(hoursDtos[1].To);
            openingHours.WednesdayOpen = TimeHoursFromHourString(hoursDtos[2].From);
            openingHours.WednesdayClose = TimeHoursFromHourString(hoursDtos[2].To);
            openingHours.ThursdayOpen = TimeHoursFromHourString(hoursDtos[3].From);
            openingHours.ThursdayClose = TimeHoursFromHourString(hoursDtos[3].To);
            openingHours.FridayOpen = TimeHoursFromHourString(hoursDtos[4].From);
            openingHours.FridayClose = TimeHoursFromHourString(hoursDtos[4].To);
            openingHours.SaturdayOpen = TimeHoursFromHourString(hoursDtos[5].From);
            openingHours.SaturdayClose = TimeHoursFromHourString(hoursDtos[5].To);
            openingHours.SundayOpen = TimeHoursFromHourString(hoursDtos[6].From);
            openingHours.SundayClose = TimeHoursFromHourString(hoursDtos[6].To);

            return openingHours;
        }
    }
}
