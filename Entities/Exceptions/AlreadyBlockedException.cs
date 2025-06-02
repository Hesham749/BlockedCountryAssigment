namespace BlockedCountryAPI.Entities.Exceptions;

public sealed class AlreadyBlockedException(string countryCode)
    : Exception($"Country {countryCode} is already blocked")
{
}