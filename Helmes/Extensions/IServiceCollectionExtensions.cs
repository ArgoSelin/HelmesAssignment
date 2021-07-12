using Helmes.Domain.Interfaces;
using Helmes.Domain.Interfaces.Services;
using Helmes.Infrastructure;
using Helmes.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Helmes.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
            .AddScoped<IShipmentService, ShipmentService>()
            .AddScoped<IParcelBagService, ParcelBagService>()
            .AddScoped<ILetterBagService, LetterBagService>()
            .AddScoped<IParcelService, ParcelService>()
            .AddScoped<IFinalBagService, FinalBagService>();
        }
    }
}
