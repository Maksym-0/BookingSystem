namespace Booking.Core.DataTransferObjects.Responses
{
    public class RevenueReportResponse
    {
        public decimal TotalRevenue { get; init; }

        // Розбивка доходу по кожному залу (назва залу -> сума)
        public Dictionary<string, decimal> RevenueByRoom { get; init; } = new();
    }
}
