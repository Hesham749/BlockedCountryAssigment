using BlockedCountryAPI.Entities.Models;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared;
using Shared.DTOs;

namespace BlockedCountryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class LogsController(ILogService logService,
    IBlockedCountryRepository repo,
    IGeoLookupService geoService) : ControllerBase
{
    [HttpGet("blocked-attempts")]
    public ActionResult<PagedResult<BlockedAttemptLog>> GetAttempts([FromQuery] PaginatedQueryParameters query)
    {
        var result = logService.GetBlockedAttempts(query);

        return Ok(result);
    }

    [HttpGet("check-block")]
    public async Task<IActionResult> CheckBlock()
    {
        var geo = await geoService.LookupIpAsync(null);

        if (geo is null)
            return StatusCode(502, "Geo lookup failed.");

        var isBlocked = repo.Exists(geo.CountryCode);

        var logEntry = new BlockedAttemptLog
        {
            IpAddress = geo.IpAddress,
            CountryCode = geo.CountryCode,
            IsBlocked = isBlocked,
            UserAgent = Request.Headers.UserAgent.ToString()
        };

        logService.LogAttempt(logEntry);

        return Ok(logEntry);
    }
}