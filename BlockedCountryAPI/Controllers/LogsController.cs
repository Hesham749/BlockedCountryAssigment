using BlockedCountryAPI.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared;
using Shared.DTOs;

namespace BlockedCountryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class LogsController(ILogService logService) : ControllerBase
{
    [HttpGet("blocked-attempts")]
    public ActionResult<PagedResult<BlockedAttemptLog>> GetAttempts([FromQuery] PaginatedQueryParameters query)
    {
        var result = logService.GetBlockedAttempts(query);

        return Ok(result);
    }
}