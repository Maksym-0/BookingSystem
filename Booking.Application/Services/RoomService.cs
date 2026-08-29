using Booking.Core;
using Booking.Core.DataTransferObjects.Requests;
using Booking.Core.DataTransferObjects.Responses;
using Booking.Core.Enums;
using Booking.Core.Interfaces.Services;
using Booking.Core.Interfaces.UnitsOfWork;
using Booking.Core.Models;

namespace Booking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<Guid>> CreateRoomAsync(RoomCreateRequest dto)
        {
            var room = new Room(dto.Name, dto.Capacity, dto.BasePricePerHour);

            if (dto.AvailableServiceIds.Any())
            {
                var services = await _unitOfWork.Services.GetByIdsAsync(dto.AvailableServiceIds);
                foreach (var service in services)
                {
                    room.AddService(service);
                }
            }

            _unitOfWork.Rooms.Add(room);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<Guid>(true, "Зал успішно створено", ErrorType.None, room.Id);
        }

        public async Task<ServiceResponse<bool>> UpdateRoomAsync(Guid roomId, RoomUpdateRequest dto)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            if (room == null)
            {
                return new ServiceResponse<bool>(false, "Зал не знайдено", ErrorType.NotFound, false);
            }

            room.UpdateDetails(newPrice: dto.NewBasePrice);

            if (dto.ServiceIdToAdd.HasValue)
            {
                var service = await _unitOfWork.Services.GetByIdAsync(dto.ServiceIdToAdd.Value);
                if (service == null)
                    return new ServiceResponse<bool>(false, "Вказану послугу для додавання не знайдено", ErrorType.NotFound, false);

                room.AddService(service);
            }

            if (dto.ServiceIdToRemove.HasValue)
            {
                room.RemoveService(dto.ServiceIdToRemove.Value);
            }

            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<bool>(true, "Дані залу оновлено", ErrorType.None, true);
        }

        public async Task<ServiceResponse<bool>> DeleteRoomAsync(Guid roomId)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            if (room == null)
            {
                return new ServiceResponse<bool>(false, "Зал не знайдено", ErrorType.NotFound, false);
            }

            _unitOfWork.Rooms.Delete(room);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<bool>(true, "Зал видалено", ErrorType.None, true);
        }

        public async Task<ServiceResponse<List<RoomResponse>>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity)
        {
            if (startTime >= endTime)
            {
                return new ServiceResponse<List<RoomResponse>>(
                    false, "Час початку має бути раніше часу завершення.", ErrorType.Validation, null);
            }

            var rooms = await _unitOfWork.Rooms.GetAvailableRoomsAsync(startTime.ToUniversalTime(), endTime.ToUniversalTime(), capacity);

            var responseData = rooms.Select(r => new RoomResponse
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                BasePricePerHour = r.BasePricePerHour,
                AvailableServices = r.AvailableServices.Select(s => new ServiceResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price
                }).ToList()
            }).ToList();

            return new ServiceResponse<List<RoomResponse>>(true, "Доступні зали знайдено", ErrorType.None, responseData);
        }
    }
}
