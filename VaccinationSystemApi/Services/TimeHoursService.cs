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
                    from = dto.from,
                    to = dto.to,
                };

                adminDtos.Add(adminDto);
            }

            return adminDtos;
        }

        public ICollection<OpeningHoursDTO> OpeningHoursToDTO(OpeningHours openingHours)
        {
            var mondayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.MondayOpen),
                to = HourStringFromTimeHours(openingHours.MondayClose),
            };
            var tuesdayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.TuesdayOpen),
                to = HourStringFromTimeHours(openingHours.TuesdayClose),
            };
            var wednesdayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.WednesdayOpen),
                to = HourStringFromTimeHours(openingHours.WednesdayClose),
            };
            var thursdayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.ThursdayOpen),
                to = HourStringFromTimeHours(openingHours.ThursdayClose),
            };
            var fridayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.FridayOpen),
                to = HourStringFromTimeHours(openingHours.FridayClose),
            };
            var saturdayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.SaturdayOpen),
                to = HourStringFromTimeHours(openingHours.SaturdayClose),
            };
            var sundayDto = new OpeningHoursDTO()
            {
                from = HourStringFromTimeHours(openingHours.SundayOpen),
                to = HourStringFromTimeHours(openingHours.SundayClose),
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
            openingHours.MondayOpen = TimeHoursFromHourString(hoursDtos[0].from);
            openingHours.MondayClose = TimeHoursFromHourString(hoursDtos[0].to);
            openingHours.TuesdayOpen = TimeHoursFromHourString(hoursDtos[1].from);
            openingHours.TuesdayClose = TimeHoursFromHourString(hoursDtos[1].to);
            openingHours.WednesdayOpen = TimeHoursFromHourString(hoursDtos[2].from);
            openingHours.WednesdayClose = TimeHoursFromHourString(hoursDtos[2].to);
            openingHours.ThursdayOpen = TimeHoursFromHourString(hoursDtos[3].from);
            openingHours.ThursdayClose = TimeHoursFromHourString(hoursDtos[3].to);
            openingHours.FridayOpen = TimeHoursFromHourString(hoursDtos[4].from);
            openingHours.FridayClose = TimeHoursFromHourString(hoursDtos[4].to);
            openingHours.SaturdayOpen = TimeHoursFromHourString(hoursDtos[5].from);
            openingHours.SaturdayClose = TimeHoursFromHourString(hoursDtos[5].to);
            openingHours.SundayOpen = TimeHoursFromHourString(hoursDtos[6].from);
            openingHours.SundayClose = TimeHoursFromHourString(hoursDtos[6].to);

            return openingHours;
        }
    }
}
