using Booking.Core.DataTransferObjects.Requests;
using Booking.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class BookingsController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Бронює зал на вказаний час та розраховує фінальну вартість.
        /// </summary>
        /// <remarks>
        /// Вартість оренди автоматично розраховується залежно від часу доби (ранкові знижки, пікові націнки тощо) 
        /// та сумується з вартістю обраних додаткових послуг.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequest request)
        {
            var result = await _bookingService.CreateBookingAsync(request);
            return HandleResult(result);
        }
    }
}
