using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Services;

public class TemporalBlockCleanupService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IBlockedCountryRepository>();

            var now = DateTime.UtcNow;
            var expiredCountries = repo.GetAllWithCount().blockedCountries
                .Where(c => c.IsTemporary && c.DurationMinutes.HasValue && c.ExpiresAt <= now)
                .ToList();

            foreach (var country in expiredCountries)
            {
                repo.Remove(country.CountryCode);
                Console.WriteLine($"[Auto-Unblock] {country.CountryCode} removed at {DateTime.UtcNow}");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}