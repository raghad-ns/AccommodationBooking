using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Domain.Cities.Services;
using AccommodationBooking.Library.Pagination.Models;
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
        returnedCity.Should().BeEquivalentTo(city);
    }

    [Fact]
    public async void InsertOne_ShouldInvokeRepositoryMethod()
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
        _cityRepositoryMock.Verify(mock => mock.InsertOne(It.IsAny<City>()), Times.Once);
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

    [Fact]
    public async void UpdateOne_ShouldUpdateCity_ReturningUpdatedCity_ValidObject()
    {
        // Arrange
        var city = _fixture.Create<City>();

        _cityRepositoryMock
            .Setup(repo => repo.UpdateOne(It.IsAny<int>(), It.IsAny<City>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.UpdateOne(city.Id, city);

        // Assert
        returnedCity.Should().BeEquivalentTo(city);
    }
    
    [Fact]
    public async void UpdateOne_ShouldInvokeRepositoryMethod()
    {
        // Arrange
        var city = _fixture.Create<City>();

        _cityRepositoryMock
            .Setup(repo => repo.UpdateOne(It.IsAny<int>(), It.IsAny<City>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.UpdateOne(city.Id, city);

        // Assert
        _cityRepositoryMock.Verify(mock => mock.UpdateOne(It.IsAny<int>(), It.IsAny<City>()), Times.Once);
    }

    [Fact]
    public async void UpdateOne_ShouldThrowException_InvalidObject()
    {
        // Arrange
        var city = _fixture.Create<City>();
        city.Name = null;

        _cityRepositoryMock
            .Setup(repo => repo.UpdateOne(It.IsAny<int>(), It.IsAny<City>()))
            .ReturnsAsync(city);

        // Act
        var act = async () => await _cityService.UpdateOne(city.Id, city);

        // Assert
        act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async void DeleteOne_ExistedRecord_ShouldSucceed()
    {
        // Arrange
        _cityRepositoryMock
            .Setup(repo => repo.DeleteOne(It.IsAny<int>()));

        // Act
        var deletion = async () => await _cityService.DeleteOne(_fixture.Create<int>());

        // Assert
        await deletion.Should().NotThrowAsync();
    }
    
    [Fact]
    public async void DeleteOne_ShouldInvokeRepositoryMethod()
    {
        // Arrange
        _cityRepositoryMock
            .Setup(repo => repo.DeleteOne(It.IsAny<int>()));

        // Act
        var deletion = async () => await _cityService.DeleteOne(_fixture.Create<int>());

        // Assert
        _cityRepositoryMock.Verify(mock => mock.DeleteOne(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async void Search_ShouldReturnListOfRecords()
    {
        // Arrange
        var cities = _fixture.CreateMany<City>().ToList();
        var paginatedCities = new PaginatedData<City>
        {
            Total = cities.Count(),
            Data = cities.AsReadOnly()
        };

        _cityRepositoryMock
            .Setup(repo => repo.Search(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CityFilters>(),
                It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync(paginatedCities);

        // Act
        var returnedCities = await _cityService.Search(
            _fixture.Create<int>(),
            _fixture.Create<int>(),
            _fixture.Create<CityFilters>(),
            default
            );

        // Assert
        returnedCities.Should().BeEquivalentTo(paginatedCities);
    }

    [Fact]
    public async void GetOne_ValidId_ShouldReturnRecord()
    {
        // Arrange
        var city = _fixture.Create<City>();
        _cityRepositoryMock
            .Setup(repo => repo.GetOne(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.GetOne(_fixture.Create<int>(), default);

        // Assert
        returnedCity
            .Should().NotBeNull()
            .And.BeEquivalentTo(city);
    }
    

    [Fact]
    public async void GetOne_ShouldInvokeRepositoryMethod()
    {
        // Arrange
        var city = _fixture.Create<City>();
        _cityRepositoryMock
            .Setup(repo => repo.GetOne(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.GetOne(_fixture.Create<int>(), default);

        // Assert
        _cityRepositoryMock.Verify(mock => mock.GetOne(It.IsAny<int>(), default), Times.Once);
    }

    [Fact]
    public async void GetOne_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var city = _fixture.Create<City>();
        _cityRepositoryMock
            .Setup(repo => repo.GetOne(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((City?)null);

        // Act
        var returnedCity = await _cityService.GetOne(_fixture.Create<int>(), default);

        // Assert
        returnedCity.Should().BeNull();
    }

    [Fact]
    public async void GetOneByName_ValidId_ShouldReturnRecord()
    {
        // Arrange
        var city = _fixture.Create<City>();
        _cityRepositoryMock
            .Setup(repo => repo.GetOneByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.GetOneByName(
            _fixture.Create<string>(),
            default
            );

        // Assert
        returnedCity
            .Should().NotBeNull()
            .And.BeEquivalentTo(city);
    }
    
    [Fact]
    public async void GetOneByName_ShouldInvokeRepositoryMethod()
    {
        // Arrange
        var city = _fixture.Create<City>();
        _cityRepositoryMock
            .Setup(repo => repo.GetOneByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        // Act
        var returnedCity = await _cityService.GetOneByName(
            city.Name,
            default
            );

        // Assert
        _cityRepositoryMock.Verify(mock => mock.GetOneByName(It.IsAny<string>(), default), Times.Once);
    }

    [Fact]
    public async void GetOneByName_InvalidId_ShouldReturnNull()
    {
        // Arrange
        var city = _fixture.Create<City>();
        _cityRepositoryMock
            .Setup(repo => repo.GetOneByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((City?)null);

        // Act
        var returnedCity = await _cityService.GetOneByName(
            _fixture.Create<string>(),
            default
            );

        // Assert
        returnedCity.Should().BeNull();
    }
}