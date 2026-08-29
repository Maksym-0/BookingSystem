namespace Booking.Core.Models
{
    public class BookingRecord // Запис бронювання
    {
        public Guid Id { get; private set; }
        public Guid RoomId { get; private set; }
        public Room? Room { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public decimal TotalPrice { get; private set; }

        private readonly List<Service> _selectedServices = new();
        public IReadOnlyCollection<Service> SelectedServices => _selectedServices.AsReadOnly();

        private BookingRecord() { }

        public BookingRecord(Guid roomId, DateTime startTime, DateTime endTime, decimal totalPrice, List<Service> selectedServices)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Час початку має бути раніше часу завершення.");

            Id = Guid.NewGuid();
            RoomId = roomId;
            StartTime = startTime.ToUniversalTime();
            EndTime = endTime.ToUniversalTime();
            TotalPrice = totalPrice;
            _selectedServices = selectedServices;
        }
    }
}
