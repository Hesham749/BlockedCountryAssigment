using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared;
using Shared.DTOs;

namespace BlockedCountryAPI.Controllers;

[Route("api/countries")]
[Produces("application/json")]
[ApiController]
public class CountriesController(ICountryBlockService service) : ControllerBase
{
    [HttpGet("blocked")]
    public ActionResult<PagedResult<BlockCountryResponse>> GetBlockedCountries([FromQuery] PaginatedBlockedCountriesQueryParameters query)
    {
        var result = service.GetBlockedCountries(query);
        return Ok(result);
    }


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
        var unBlockCountryRequest = new UnBlockCountryRequest { CountryCode = countryCode };

        if (!TryValidateModel(unBlockCountryRequest))
            return UnprocessableEntity(ModelState);

        service.UnBlockCountry(unBlockCountryRequest);

        return NoContent();
    }
}