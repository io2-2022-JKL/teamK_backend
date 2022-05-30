using VaccinationSystemApi.Dtos.Doctors;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Helpers.Converters
{
    public static class ConvertTimeSlotToResponse
    {
        public static GetTimeSlotsResponse Convert(TimeSlot timeSlot)
        {
            GetTimeSlotsResponse response = new()
            {
                Id = timeSlot.Id.ToString(),
                From = timeSlot.From.ToString("dd-MM-yyyy HH:mm"),
                To = timeSlot.To.ToString("dd-MM-yyyy HH:mm"),
                IsFree = timeSlot.IsFree,
            };

            return response;
        }
    }
}
