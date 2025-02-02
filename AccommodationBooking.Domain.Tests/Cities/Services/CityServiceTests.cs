using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Domain.Cities.Services;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace AccommodationBooking.Domain.Tests.Cities.Services;

public class CityServiceTests
{
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IValidator<City>> _validatorMock;
    private readonly ICityService _cityService;
    private readonly Fixture _fixture;

    public CityServiceTests()
    {
        _cityRepositoryMock = new Mock<ICityRepository>();
        _validatorMock = new Mock<IValidator<City>>();
        _fixture = new Fixture();
        _cityService = new CityService(_cityRepositoryMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async void InsertOne_ShouldInsertCity_ValidObject()
    {
        // Arrange
        var city = _fixture.Create<City>();

        _cityRepositoryMock
            .Setup(repo => repo.InsertOne(It.IsAny<City>()))
            .ReturnsAsync(city.Id);
        _cityRepositoryMock
            .Setup(repo => repo.GetOne(city.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.InsertOne(city, new CancellationToken());

        // Assert
        returnedCity.Should().NotBeNull().And.Be(city);
    }

    [Fact]
    public async void InsertOne_ShouldThrowException_InvalidObject()
    {
        // Arrange
        var city = _fixture.Create<City>();
        city.Name = null;

        _cityRepositoryMock
            .Setup(repo => repo.InsertOne(It.IsAny<City>()))
            .ReturnsAsync(city.Id);
        _cityRepositoryMock
            .Setup(repo => repo.GetOne(city.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        // Act
        var act = async () => await _cityService.InsertOne(city, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<Exception>();
    }
}