namespace BlockedCountryAPI.Entities.Exceptions;

public sealed class InvalidCountryCodeException(string countryCode)
    : Exception($"{countryCode} is invalid country code")
{
}