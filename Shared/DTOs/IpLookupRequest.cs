using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public record IpLookupRequest
{
    [RegularExpression(@"^((25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\.){3}(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$",
        ErrorMessage = "Invalid IP address format.")]
    public string? IpAddress { get; init; }
}