namespace Booking.Core.Models
{
    public class Service // Послуги
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public List<Room> Rooms { get; private set; } = new();
        public List<BookingRecord> Bookings { get; private set; } = new();

        private Service() { }

        public Service(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва послуги не може бути порожньою.");
            if (price < 0)
                throw new ArgumentException("Ціна не може бути від'ємною.");

            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Ціна не може бути від'ємною.");

            Price = newPrice;
        }
    }
}
