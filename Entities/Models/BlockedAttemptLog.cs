namespace BlockedCountryAPI.Entities.Models;

public record BlockedAttemptLog
{
    public string IpAddress { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
    public string CountryCode { get; init; } = string.Empty;
    public bool IsBlocked { get; init; }
    public string UserAgent { get; init; } = string.Empty;
}