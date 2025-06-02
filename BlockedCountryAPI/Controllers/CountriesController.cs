using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTOs;

namespace BlockedCountryAPI.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController(ICountryBlockService service) : ControllerBase
{
    [HttpPost("block")]
    public IActionResult BlockCountry([FromBody] BlockCountryRequest request)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        return Ok(service.BlockCountry(request));
    }

    [HttpPost("temporal-block")]
    public IActionResult TemporaryBlockCountry([FromBody] TemporalBlockRequest request)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        return Ok(service.BlockTemporarily(request));
    }
}