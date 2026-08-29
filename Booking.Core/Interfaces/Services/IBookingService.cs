using Booking.Core.DataTransferObjects.Requests;
using Booking.Core.DataTransferObjects.Responses;

namespace Booking.Core.Interfaces.Services
{
    public interface IBookingService
    {
        Task<ServiceResponse<BookingResponse>> CreateBookingAsync(BookingCreateRequest request);
    }
}
