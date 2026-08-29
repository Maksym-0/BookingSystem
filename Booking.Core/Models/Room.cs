namespace Booking.Core.Models
{
    public class Room // Зал для бронювання
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public decimal BasePricePerHour { get; private set; }

        private readonly List<Service> _availableServices = new();
        public IReadOnlyCollection<Service> AvailableServices => _availableServices.AsReadOnly();

        private readonly List<BookingRecord> _bookings = new();
        public IReadOnlyCollection<BookingRecord> Bookings => _bookings.AsReadOnly();

        private Room() { }

        public Room(string name, int capacity, decimal basePricePerHour)
        {
            if (capacity <= 0) 
                throw new ArgumentException("Місткість має бути більше 0.");
            if (basePricePerHour < 0) 
                throw new ArgumentException("Ціна не може бути від'ємною.");

            Id = Guid.NewGuid();
            Name = name;
            Capacity = capacity;
            BasePricePerHour = basePricePerHour;
        }

        public void UpdateDetails(int? newCapacity = null, decimal? newPrice = null)
        {
            if (newCapacity.HasValue && newCapacity.Value > 0)
                Capacity = newCapacity.Value;

            if (newPrice.HasValue && newPrice.Value >= 0)
                BasePricePerHour = newPrice.Value;
        }

        public void AddService(Service service)
        {
            if (!_availableServices.Any(s => s.Id == service.Id))
            {
                _availableServices.Add(service);
            }
        }

        public void RemoveService(Guid serviceId)
        {
            var service = _availableServices.FirstOrDefault(s => s.Id == serviceId);
            if (service != null)
            {
                _availableServices.Remove(service);
            }
        }
    }
}
