namespace Shared.Options;

public record GeoApiOptions
{
    public string BaseUrl { get; init; } = "https://ipapi.co";
}