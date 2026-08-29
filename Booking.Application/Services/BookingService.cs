using Booking.Core;
using Booking.Core.DataTransferObjects.Requests;
using Booking.Core.DataTransferObjects.Responses;
using Booking.Core.Enums;
using Booking.Core.Interfaces.Services;
using Booking.Core.Interfaces.UnitsOfWork;
using Booking.Core.Models;

namespace Booking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentCalculatorService _calculatorService;

        public BookingService(IUnitOfWork unitOfWork, IRentCalculatorService calculatorService)
        {
            _unitOfWork = unitOfWork;
            _calculatorService = calculatorService;
        }

        public async Task<ServiceResponse<BookingResponse>> CreateBookingAsync(BookingCreateRequest request)
        {
            DateTime endTime = request.StartTime.Add(request.Duration);

            var room = await _unitOfWork.Rooms.GetByIdAsync(request.RoomId);
            if (room == null)
                return new ServiceResponse<BookingResponse>(false, "Зал не знайдено", ErrorType.NotFound, null);

            bool isBooked = await _unitOfWork.Bookings.IsRoomBookedAsync(request.RoomId, request.StartTime, endTime);
            if (isBooked)
                return new ServiceResponse<BookingResponse>(false, "На жаль, зал вже заброньовано на цей час.", ErrorType.Conflict, null);

            var selectedServices = new List<Service>();
            decimal servicesTotalPrice = 0;

            if (request.SelectedServiceIds.Any())
            {
                selectedServices = await _unitOfWork.Services.GetByIdsAsync(request.SelectedServiceIds);

                var availableServiceIds = room.AvailableServices.Select(s => s.Id).ToList();
                if (selectedServices.Any(s => !availableServiceIds.Contains(s.Id)))
                {
                    return new ServiceResponse<BookingResponse>(false, "Обрана послуга недоступна для цього залу.", ErrorType.Validation, null);
                }

                servicesTotalPrice = selectedServices.Sum(s => s.Price);
            }

            var rentResponse = _calculatorService.CalculateRoomRent(room.BasePricePerHour, request.StartTime, endTime);

            if (!rentResponse.Success)
            {
                return new ServiceResponse<BookingResponse>(false, rentResponse.Message, rentResponse.Error, null);
            }

            decimal roomRentPrice = rentResponse.Data;

            decimal finalPrice = roomRentPrice + servicesTotalPrice;

            var booking = new BookingRecord(
                roomId: room.Id,
                startTime: request.StartTime,
                endTime: endTime,
                totalPrice: finalPrice,
                selectedServices: selectedServices
            );

            _unitOfWork.Bookings.Add(booking);
            await _unitOfWork.SaveChangesAsync();

            var response = new BookingResponse
            {
                BookingId = booking.Id,
                RoomId = booking.RoomId,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                TotalPrice = booking.TotalPrice
            };

            return new ServiceResponse<BookingResponse>(true, "Успішно заброньовано!", ErrorType.None, response);
        }
    }
}
