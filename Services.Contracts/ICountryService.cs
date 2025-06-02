namespace Services.Contracts;

public interface ICountryService
{
    string GetCountryName(string countryCode);
}