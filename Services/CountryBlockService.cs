using BlockedCountryAPI.Entities.Exceptions;
using BlockedCountryAPI.Entities.Models;
using Contracts;
using Services.Contracts;
using Services.Mapping;
using Shared.DTOs;

namespace Services;

public class CountryBlockService(ICountryService countryService,
    IBlockedCountryRepository countryRepository)
    : ICountryBlockService
{
    private readonly MappingProfile _mapper = new();

    public BlockCountryResponse BlockCountry(BlockCountryRequest blockCountryRequest)
    {
        var countryName = countryService.GetCountryName(blockCountryRequest.CountryCode);

        var blockedCountry = new BlockedCountry(blockCountryRequest.CountryCode, countryName);

        var addedCountry = countryRepository.Add(blockedCountry);

        if (!addedCountry)
            throw new AlreadyBlockedException(blockCountryRequest.CountryCode);

        return _mapper.ToDto(blockedCountry);
    }

    public BlockCountryResponse BlockTemporarily(TemporalBlockRequest blockCountryRequest)
    {
        var countryName = countryService.GetCountryName(blockCountryRequest.CountryCode);

        var blockedCountry = new BlockedCountry
            (blockCountryRequest.CountryCode, countryName, blockCountryRequest.DurationMinutes);

        var addedCountry = countryRepository.Add(blockedCountry);

        if (!addedCountry)
            throw new AlreadyBlockedException(blockCountryRequest.CountryCode);

        return _mapper.ToDto(blockedCountry);
    }

    public void UnBlockCountry(UnBlockCountryRequest unBlockCountryRequest)
    {
        var unBlockedCountry = countryRepository.Remove(unBlockCountryRequest.CountryCode);

        if (!unBlockedCountry)
            throw new CountryNotBlockedException(unBlockCountryRequest.CountryCode);
    }
}