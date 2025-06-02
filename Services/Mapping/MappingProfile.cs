using BlockedCountryAPI.Entities.Models;
using Riok.Mapperly.Abstractions;
using Shared.DTOs;

namespace Services.Mapping;

[Mapper]
public partial class MappingProfile
{

    public partial BlockCountryResponse ToDto(BlockedCountry blockedCountry);
}