using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Repositories;
using AccommodationBooking.Domain.Hotels.Services;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace AccommodationBooking.Domain.Tests.Hotels.Services;

public class HotelServiceTests
{

    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IValidator<Hotel>> _validatorMock;
    private readonly IHotelService _hotelService;
    private readonly Fixture _fixture;

    public HotelServiceTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _validatorMock = new Mock<IValidator<Hotel>>();
        _fixture = new Fixture();
        _hotelService = new HotelService(_hotelRepositoryMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async void InsertOne_ShouldInsertHotel_ValidObject()
    {
        // Arrange
        var hotel = _fixture.Create<Hotel>();

        _hotelRepositoryMock
            .Setup(repo => repo.InsertOne(It.IsAny<Hotel>()))
            .ReturnsAsync(hotel.Id);
        _hotelRepositoryMock
            .Setup(repo => repo.GetOne(hotel.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        // Act
        var returnedHotel = await _hotelService.InsertOne(hotel, new CancellationToken());

        // Assert
        returnedHotel.Should().NotBeNull().And.Be(hotel);
    }

    [Fact]
    public async void InsertOne_ShouldThrowException_InvalidObject()
    {
        // Arrange
        var hotel = _fixture.Create<Hotel>();
        hotel.Name = null;

        _hotelRepositoryMock
            .Setup(repo => repo.InsertOne(It.IsAny<Hotel>()))
            .ReturnsAsync(hotel.Id);
        _hotelRepositoryMock
            .Setup(repo => repo.GetOne(hotel.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        // Act
        var act = async () => await _hotelService.InsertOne(hotel, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<Exception>();
    }
}