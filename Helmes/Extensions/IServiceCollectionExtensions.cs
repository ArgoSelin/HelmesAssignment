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
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("Server=mssql4.websitelive.net;Database=armediadesign_helmes;Uid=armediadesign_helmes;Password=HelmesDatabase1!;Connection Timeout=30;", b => b.MigrationsAssembly("Helmes.Api")));

            services.AddScoped<Func<DatabaseContext>>((provider) => () => provider.GetService<DatabaseContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

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
