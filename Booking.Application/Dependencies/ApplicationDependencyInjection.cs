using Booking.Application.Services;
using Booking.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application.Dependencies
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationLogic(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<IRentCalculatorService, RentCalculatorService>();

            return services;
        }
    }
}
