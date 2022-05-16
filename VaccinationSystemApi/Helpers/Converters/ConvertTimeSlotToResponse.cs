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
                From = timeSlot.From.ToString(),
                To = timeSlot.To.ToString(),
                IsFree = timeSlot.IsFree,
            };

            return response;
        }
    }
}
