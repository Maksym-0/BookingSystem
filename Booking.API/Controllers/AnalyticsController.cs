using Booking.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class AnalyticsController : BaseController
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        /// <summary>
        /// Звіт про загальний дохід компанії.
        /// </summary>
        /// <remarks>
        /// Показує сумарний дохід за обраний період та розбивку доходу по кожному залу окремо.
        /// </remarks>
        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenueReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _analyticsService.GetRevenueReportAsync(startDate, endDate);
            return HandleResult(result);
        }

        /// <summary>
        /// Звіт про завантаженість залів.
        /// </summary>
        /// <remarks>
        /// Показує, скільки годин був орендований кожен зал за обраний період. 
        /// Зали відсортовані за популярністю (від найбільшого значення до найменшого).
        /// </remarks>
        [HttpGet("occupancy")]
        public async Task<IActionResult> GetRoomOccupancy([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _analyticsService.GetRoomOccupancyAsync(startDate, endDate);
            return HandleResult(result);
        }
    }
}
