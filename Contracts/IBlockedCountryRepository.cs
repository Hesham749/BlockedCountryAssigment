using BlockedCountryAPI.Entities.Models;

namespace Contracts;

public interface IBlockedCountryRepository
{
    bool Add(BlockedCountry country);

    bool Remove(string countryCode);

    bool Exists(string countryCode);

    BlockedCountry? GetByCode(string countryCode);

    IEnumerable<BlockedCountry> GetAll();
}