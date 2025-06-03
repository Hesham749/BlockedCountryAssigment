using BlockedCountryAPI.Entities.Exceptions;
using BlockedCountryAPI.Entities.Models;
using Contracts;
using Moq;
using Services;
using Services.Contracts;
using Shared.DTOs;

namespace BlockedCountryAPI.Tests.Services;

public class CountryBlockServiceTests
{
    private readonly Mock<ICountryService> _countryServiceMock = new();
    private readonly Mock<IBlockedCountryRepository> _repoMock = new();
    private readonly CountryBlockService _service;

    public CountryBlockServiceTests()
    {
        _service = new CountryBlockService(
            _countryServiceMock.Object,
            _repoMock.Object
        );
    }

    [Fact]
    public void BlockCountry_Should_Add_Country_When_NotExists()
    {
        // Arrange
        var request = new BlockCountryRequest { CountryCode = "US" };

        _repoMock.Setup(r => r.Exists(request.CountryCode)).Returns(false);
        _repoMock.Setup(r => r.Add(It.IsAny<BlockedCountry>())).Returns(true);

        // Act
        var result = _service.BlockCountry(request);

        // Assert
        Assert.Equal("US", result.CountryCode);
        Assert.False(result.IsTemporary);
        _repoMock.Verify(r => r.Add(It.IsAny<BlockedCountry>()), Times.Once);
    }

    [Fact]
    public void BlockCountry_Should_Throw_When_Country_Already_Exists()
    {
        // Arrange
        var request = new BlockCountryRequest { CountryCode = "EG" };
        _repoMock.Setup(r => r.Exists(request.CountryCode)).Returns(true);

        // Act & Assert
        var ex = Assert.Throws<AlreadyBlockedException>(() =>
        {
            _service.BlockCountry(request);
        });

        Assert.Equal($"Country {request.CountryCode} is already blocked.", ex.Message);
        _repoMock.Verify(r => r.Add(It.IsAny<BlockedCountry>()), Times.Never);
    }
}