using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public record PaginatedQueryParameters
{
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
    public int Page { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "{0} must be between 1 to 20")]
    public int PageSize { get; init; } = 20;

    public string? CountryCode { get; init; }

    public string? CountryName { get; init; }
}