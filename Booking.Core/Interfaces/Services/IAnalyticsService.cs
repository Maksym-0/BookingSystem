using Booking.Core.DataTransferObjects.Responses;

namespace Booking.Core.Interfaces.Services
{
    public interface IAnalyticsService
    {
        Task<ServiceResponse<RevenueReportResponse>> GetRevenueReportAsync(DateTime startDate, DateTime endDate);
        Task<ServiceResponse<List<RoomOccupancyResponse>>> GetRoomOccupancyAsync(DateTime startDate, DateTime endDate);
    }
}
