using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public record TemporalBlockRequest(
       [Required(ErrorMessage = "Country code is required")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Country code must be exactly 2 characters")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Country code must be 2 uppercase letters")]
        string CountryCode,

       [Required(ErrorMessage = "Duration is required")]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes (24 hours)")]
        int DurationMinutes
   );