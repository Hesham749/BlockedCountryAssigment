using System.Collections.Concurrent;
using BlockedCountryAPI.Entities.Models;
using Contracts;

namespace Infrastructure;

public class BlockedCountryRepository : IBlockedCountryRepository
{
    private readonly ConcurrentDictionary<string, BlockedCountry> _blockedCountries = new();

    public bool Add(BlockedCountry country) => _blockedCountries.TryAdd(country.CountryCode.ToUpper(), country);

    public bool Remove(string countryCode) => _blockedCountries.TryRemove(countryCode.ToUpper(), out _);

    public bool Exists(string countryCode) => _blockedCountries.ContainsKey(countryCode.ToUpper());

    public BlockedCountry? GetByCode(string countryCode)
    {
        _blockedCountries.TryGetValue(countryCode.ToUpper(), out var country);
        return country;
    }

    public (int total, IEnumerable<BlockedCountry> blockedCountries) GetAllWithCount(int page = 1, int pageSize = 20, string? countryCode = null, string? countryName = null)
    {
        var query = _blockedCountries.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(countryCode))
            query = query.Where(c => c.CountryCode.Contains(countryCode, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(countryName))
            query = query.Where(c => c.CountryName.Contains(countryName, StringComparison.OrdinalIgnoreCase));

        var total = query.Count();

        var blockedCountries = query.Skip((page - 1) * pageSize).Take(pageSize);

        return (total, blockedCountries);
    }
}