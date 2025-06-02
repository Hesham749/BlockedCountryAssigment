using BlockedCountryAPI.Entities.Exceptions;
using Services.Contracts;

namespace Services;

public class CountryService : ICountryService
{
    public string GetCountryName(string countryCode)
    {
        var country = ISO3166.Country.List.FirstOrDefault(c => c.TwoLetterCode
                                .Equals(countryCode, StringComparison.OrdinalIgnoreCase));

        return country?.Name ?? throw new InvalidCountryCodeException(countryCode);
    }
}