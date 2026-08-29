using Booking.Core.DataTransferObjects.Requests;
using Booking.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class RoomsController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Створює новий конференц-зал.
        /// </summary>
        /// <remarks>
        /// Можна одразу додати до залу доступні послуги (наприклад, Проєктор), передавши їхні ID у масиві AvailableServiceIds.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreateRequest request)
        {
            var result = await _roomService.CreateRoomAsync(request);
            return HandleResult(result);
        }

        /// <summary>
        /// Оновлює дані існуючого конференц-залу.
        /// </summary>
        /// <remarks>
        /// Дозволяє змінити базову вартість оренди, а також додати або видалити послугу для цього залу.
        /// Усі поля є необов'язковими (можна передати тільки те, що треба змінити).
        /// </remarks>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomUpdateRequest request)
        {
            var result = await _roomService.UpdateRoomAsync(id, request);
            return HandleResult(result);
        }

        /// <summary>
        /// Видаляє конференц-зал із системи.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            return HandleResult(result);
        }

        /// <summary>
        /// Пошук доступних залів на вказаний період.
        /// </summary>
        /// <remarks>
        /// Повертає список залів, місткість яких більше або дорівнює вказаній, 
        /// і які не мають перетинів з існуючими бронюваннями у заданий час.
        /// </remarks>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms(
            [FromQuery] DateTime startTime,
            [FromQuery] DateTime endTime,
            [FromQuery] int capacity)
        {
            var result = await _roomService.GetAvailableRoomsAsync(startTime, endTime, capacity);
            return HandleResult(result);
        }
    }
}
