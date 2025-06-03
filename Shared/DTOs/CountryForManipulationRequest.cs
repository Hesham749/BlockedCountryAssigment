using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public record BlockedCountryForManipulationRequest
{
    [Required(ErrorMessage = "Country code is required")]
    [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Country code must be 2 uppercase letters")]
    public string CountryCode { get; init; } = string.Empty;
}