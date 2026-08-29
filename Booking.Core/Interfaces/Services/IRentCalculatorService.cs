namespace Booking.Core.Interfaces.Services
{
    public interface IRentCalculatorService
    {
        ServiceResponse<decimal> CalculateRoomRent(decimal basePricePerHour, DateTime startTime, DateTime endTime);
    }
}
