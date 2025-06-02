using BlockedCountryAPI.Extensions;
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
        if (request is null)
            return BadRequest($"{nameof(BlockCountryRequest)} object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        return Ok(service.BlockCountry(request));
    }

    [HttpPost("temporal-block")]
    public IActionResult TemporaryBlockCountry([FromBody] TemporalBlockRequest request)
    {
        if (request is null)
            return BadRequest($"{nameof(TemporalBlockRequest)} object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        return Ok(service.BlockTemporarily(request));
    }

    [HttpDelete("block/{countryCode}")]
    public IActionResult UnBlockCountry([FromRoute] string countryCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            return BadRequest($"{nameof(countryCode)} can't be null or empty.");

        var unBlockContryRequest = new UnBlockCountryRequest { CountryCode = countryCode };

        if (!ModelState.TryValidateObjectAndAddErrors(unBlockContryRequest))
            return UnprocessableEntity(ModelState);

        service.UnBlockCountry(unBlockContryRequest);

        return NoContent();
    }
}