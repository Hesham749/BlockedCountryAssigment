using Contracts;
using Infrastructure;
using Services;
using Services.Contracts;

namespace BlockedCountryAPI.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IBlockedCountryRepository, BlockedCountryRepository>();

        services.AddScoped<ICountryBlockService, CountryBlockService>();

        services.AddSingleton<ICountryService, CountryService>();

        services.AddHostedService<TemporalBlockCleanupService>();

        services.AddHttpClient<IGeoLookupService, GeoLookupService>();
    }
}