using BlockedCountryAPI.Entities.Models;

namespace Contracts;

public interface IBlockedCountryRepository
{
    bool Add(BlockedCountry country);

    bool Remove(string countryCode);

    bool Exists(string countryCode);

    BlockedCountry? GetByCode(string countryCode);

    (int total, IEnumerable<BlockedCountry> blockedCountries) GetAllWithCount(int page = 1, int pageSize = 20, string? countryCode = null, string? countryName = null);
}