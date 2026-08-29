using Booking.Core;
using Booking.Core.Enums;
using Booking.Core.Interfaces.Services;

namespace Booking.Application.Services
{
    public class RentCalculatorService : IRentCalculatorService
    {
        public ServiceResponse<decimal> CalculateRoomRent(decimal basePricePerHour, DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
            {
                return new ServiceResponse<decimal>(false, "Час початку має бути меншим за час закінчення.", ErrorType.Validation, 0);
            }

            decimal totalPrice = 0;
            DateTime currentTime = startTime;

            while (currentTime < endTime)
            {
                var timeOfDay = TimeOnly.FromDateTime(currentTime);

                if (timeOfDay >= Constants.EveningEnd || timeOfDay < Constants.MorningStart)
                {
                    return new ServiceResponse<decimal>(
                        false,
                        "Бронювання в нічний час (з 23:00 до 06:00) заборонено.",
                        ErrorType.Validation,
                        0);
                }

                DateTime nextZoneEnd = GetNextZoneEnd(currentTime);

                DateTime intervalEnd = nextZoneEnd < endTime ? nextZoneEnd : endTime;

                decimal hoursInInterval = (decimal)(intervalEnd - currentTime).TotalHours;

                decimal multiplier = GetMultiplier(timeOfDay);

                totalPrice += basePricePerHour * hoursInInterval * multiplier;

                currentTime = intervalEnd;
            }

            return new ServiceResponse<decimal>(true, "Успішно розраховано", ErrorType.None, Math.Round(totalPrice, 2));
        }

        private decimal GetMultiplier(TimeOnly time)
        {
            if (time >= Constants.MorningStart && time < Constants.StandardStart) return Constants.MorningDiscount;
            if (time >= Constants.StandardStart && time < Constants.PeakStart) return 1.0m;
            if (time >= Constants.PeakStart && time < Constants.PeakEnd) return Constants.PeakHourMarkup;
            if (time >= Constants.PeakEnd && time < Constants.StandardEnd) return 1.0m;
            if (time >= Constants.StandardEnd && time < Constants.EveningEnd) return Constants.EveningDiscount;

            return 1.0m;
        }

        private DateTime GetNextZoneEnd(DateTime current)
        {
            var time = TimeOnly.FromDateTime(current);
            var date = current.Date;

            if (time >= Constants.MorningStart && time < Constants.StandardStart)
                return date.Add(Constants.StandardStart.ToTimeSpan());

            if (time >= Constants.StandardStart && time < Constants.PeakStart)
                return date.Add(Constants.PeakStart.ToTimeSpan());

            if (time >= Constants.PeakStart && time < Constants.PeakEnd)
                return date.Add(Constants.PeakEnd.ToTimeSpan());

            if (time >= Constants.PeakEnd && time < Constants.StandardEnd)
                return date.Add(Constants.StandardEnd.ToTimeSpan());

            if (time >= Constants.StandardEnd && time < Constants.EveningEnd)
                return date.Add(Constants.EveningEnd.ToTimeSpan());

            return date.Add(Constants.MorningStart.ToTimeSpan());
        }
    }
}
