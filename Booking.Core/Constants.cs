namespace Booking.Core
{
    public static class Constants
    {
        // Множники
        public const decimal MorningDiscount = 0.9m; // Знижка 10%
        public const decimal EveningDiscount = 0.8m; // Знижка 20%
        public const decimal PeakHourMarkup = 1.15m; // Націнка 15%

        // Часові рамки
        public static readonly TimeOnly MorningStart = new(6, 0);
        public static readonly TimeOnly StandardStart = new(9, 0);
        public static readonly TimeOnly PeakStart = new(12, 0);
        public static readonly TimeOnly PeakEnd = new(14, 0);
        public static readonly TimeOnly StandardEnd = new(18, 0);
        public static readonly TimeOnly EveningEnd = new(23, 0);
    }
}
