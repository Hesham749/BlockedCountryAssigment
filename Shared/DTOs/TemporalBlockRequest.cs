using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public record TemporalBlockRequest : BlockedCountryForManipulationRequest
{
    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes (24 hours)")]
    public int DurationMinutes { get; init; }
}