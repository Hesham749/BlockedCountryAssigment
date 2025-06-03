namespace BlockedCountryAPI.Entities.Exceptions;

public sealed class CountryNotBlockedException(string countryCode)
    : NotFoundException($"Country {countryCode} is not blocked.")
{

}