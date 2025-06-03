using Shared.DTOs;

namespace Services.Contracts;

public interface IGeoLookupService
{
    Task<IpLookupResponse?> LookupIpAsync(string? ip);
}