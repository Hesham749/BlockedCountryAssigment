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


    [Fact]
    public void UnBlockCountry_Should_Throw_When_Country_Does_Not_Exist()
    {
        // Arrange
        var request = new UnBlockCountryRequest { CountryCode = "FR" };
        _repoMock.Setup(r => r.Exists(request.CountryCode)).Returns(false);

        // Act & Assert
        var ex = Assert.Throws<CountryNotBlockedException>(() => _service.UnBlockCountry(request));

        Assert.Equal($"Country {request.CountryCode} is not blocked.", ex.Message);
        _repoMock.Verify(r => r.Remove(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void UnBlockCountry_Should_Throw_When_Remove_Fails()
    {
        // Arrange
        var request = new UnBlockCountryRequest { CountryCode = "DE" };
        _repoMock.Setup(r => r.Exists(request.CountryCode)).Returns(true);
        _repoMock.Setup(r => r.Remove(request.CountryCode)).Returns(false);

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => _service.UnBlockCountry(request));

        Assert.Equal("Failed to remove country", ex.Message);
        _repoMock.Verify(r => r.Remove(request.CountryCode), Times.Once);
    }

    [Fact]
    public void UnBlockCountry_Should_Remove_When_Country_Exists()
    {
        // Arrange
        var request = new UnBlockCountryRequest { CountryCode = "JP" };
        _repoMock.Setup(r => r.Exists(request.CountryCode)).Returns(true);
        _repoMock.Setup(r => r.Remove(request.CountryCode)).Returns(true);

        // Act
        _service.UnBlockCountry(request);

        // Assert
        _repoMock.Verify(r => r.Remove(request.CountryCode), Times.Once);
    }
}