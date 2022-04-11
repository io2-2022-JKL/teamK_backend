using System;

namespace VaccinationSystemApi.Helpers
{
    public static class TimeSlotValidator
    {
        public static bool IsAvailable(DateTime startSlot, DateTime endSlot, DateTime startDate, DateTime endDate)
        {
            if (startSlot >= startDate && startSlot <= endDate)
                return false;
            if (endSlot >= startDate && endSlot <= endDate)
                return false;
            return true;
        }
        public static void Validate(DateTime startDate, DateTime endDate)
        {
            if (startDate < DateTime.UtcNow.Date) throw new InvalidOperationException($"startDay: {startDate} cannot be ealier than today! ");
            if (endDate < startDate) throw new InvalidOperationException($"endDate {endDate} cannot be ealier than startDay: {startDate}! ");
        }
    }
}
