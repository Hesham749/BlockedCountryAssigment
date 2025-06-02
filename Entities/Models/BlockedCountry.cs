namespace BlockedCountryAPI.Entities.Models;

public class BlockedCountry
{
    public BlockedCountry(string countryCode, string countryName)
    {
        CountryCode = countryCode.ToUpper();
        CountryName = countryName;
        BlockedAt = DateTime.UtcNow;
        IsTemporary = false;
        ExpiresAt = null;
        DurationMinutes = null;
    }

    public BlockedCountry(string countryCode, string countryName, int durationMinutes)
    {
        CountryCode = countryCode.ToUpper();
        CountryName = countryName;
        BlockedAt = DateTime.UtcNow;
        IsTemporary = true;
        DurationMinutes = durationMinutes;
        ExpiresAt = DateTime.UtcNow.AddMinutes(durationMinutes);
    }

    public BlockedCountry()
    { }

    public string CountryCode { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;

    public DateTime BlockedAt { get; set; }

    public bool IsTemporary { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int? DurationMinutes { get; set; }
}