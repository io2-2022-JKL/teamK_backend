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
                id = timeSlot.Id.ToString(),
                from = timeSlot.From.ToString("dd-MM-yyyy HH:mm"),
                to = timeSlot.To.ToString("dd-MM-yyyy HH:mm"),
                isFree = timeSlot.IsFree,
            };

            return response;
        }
    }
}
