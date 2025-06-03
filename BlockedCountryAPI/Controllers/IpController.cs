using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTOs;

namespace BlockedCountryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IpController(IGeoLookupService geoService) : ControllerBase
{
    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup([FromQuery] IpLookupRequest request)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var result = await geoService.LookupIpAsync(request.IpAddress);

        if (result is null)
            return NotFound("Could not find information for the provided IP address.");

        return Ok(result);
    }
}