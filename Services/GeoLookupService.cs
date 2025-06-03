using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Services.Contracts;
using Shared.DTOs;
using Shared.Options;

namespace Services;

public class GeoLookupService(HttpClient httpClient, IOptions<GeoApiOptions> options) : IGeoLookupService
{
    private readonly GeoApiOptions _options = options.Value;

    public async Task<IpLookupResponse?> LookupIpAsync(string? ip)
    {
        var url = string.IsNullOrWhiteSpace(ip)
      ? $"{_options.BaseUrl}/json/"
      : $"{_options.BaseUrl}/{ip}/json/";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(json);

        return new IpLookupResponse(
             obj["ip"]?.ToString() ?? ip ?? "??",
             obj["country_name"]?.ToString() ?? "Unknown",
             obj["country_code"]?.ToString() ?? "??",
             obj["org"]?.ToString() ?? "Unknown"
        );
    }
}