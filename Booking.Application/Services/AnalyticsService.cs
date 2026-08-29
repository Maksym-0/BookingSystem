using Booking.Core;
using Booking.Core.DataTransferObjects.Responses;
using Booking.Core.Enums;
using Booking.Core.Interfaces.Services;
using Booking.Core.Interfaces.UnitsOfWork;

namespace Booking.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<RevenueReportResponse>> GetRevenueReportAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                return new ServiceResponse<RevenueReportResponse>(false, "Початкова дата має бути раніше кінцевої.", ErrorType.Validation, null);

            var startUtc = startDate.ToUniversalTime();
            var endUtc = endDate.ToUniversalTime();

            var totalRevenue = await _unitOfWork.Bookings.GetTotalRevenueAsync(startUtc, endUtc);
            var revenueByRoom = await _unitOfWork.Bookings.GetRevenueByRoomAsync(startUtc, endUtc);

            var report = new RevenueReportResponse
            {
                TotalRevenue = totalRevenue,
                RevenueByRoom = revenueByRoom
            };

            return new ServiceResponse<RevenueReportResponse>(true, "Звіт з доходу сформовано", ErrorType.None, report);
        }

        public async Task<ServiceResponse<List<RoomOccupancyResponse>>> GetRoomOccupancyAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                return new ServiceResponse<List<RoomOccupancyResponse>>(false, "Початкова дата має бути раніше кінцевої.", ErrorType.Validation, null);

            var occupancyDict = await _unitOfWork.Bookings.GetRoomOccupancyHoursAsync(startDate.ToUniversalTime(), endDate.ToUniversalTime());

            var utilization = occupancyDict
                .Select(kvp => new RoomOccupancyResponse
                {
                    RoomName = kvp.Key,
                    TotalHoursBooked = kvp.Value
                })
                .OrderByDescending(x => x.TotalHoursBooked)
                .ToList();

            return new ServiceResponse<List<RoomOccupancyResponse>>(true, "Звіт із завантаженості сформовано", ErrorType.None, utilization);
        }
    }
}
