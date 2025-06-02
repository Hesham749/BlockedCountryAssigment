using Shared.DTOs;

namespace Services.Contracts;

public interface ICountryBlockService
{
    BlockCountryResponse BlockCountry(BlockCountryRequest blockCountryRequest);

    BlockCountryResponse BlockTemporarily(TemporalBlockRequest blockCountryRequest);
}