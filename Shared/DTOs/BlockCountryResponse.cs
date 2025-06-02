namespace Shared.DTOs;

public record BlockCountryResponse(
    string CountryCode,
    string CountryName,
    bool IsTemporary,
    DateTime BlockedAt,
    DateTime? ExpiresAt,
    int? DurationMinutes
);