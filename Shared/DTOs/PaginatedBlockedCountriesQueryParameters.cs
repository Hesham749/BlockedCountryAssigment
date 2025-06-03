namespace Shared.DTOs;

public record PaginatedBlockedCountriesQueryParameters : PaginatedQueryParameters
{
    public string? CountryCode { get; init; }

    public string? CountryName { get; init; }
}