namespace Shared.DTOs;

public record IpLookupResponse(
    string IpAddress,
    string CountryName,
    string CountryCode,
    string ISP
);