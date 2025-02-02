using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Repositories;
using AccommodationBooking.Domain.Rooms.Services;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace AccommodationBooking.Domain.Tests.Rooms.Services;

public class RoomServiceTests
{
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IValidator<Room>> _validatorMock;
    private readonly IRoomService _roomService;
    private readonly Fixture _fixture;

    public RoomServiceTests()
    {
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _validatorMock = new Mock<IValidator<Room>>();
        _fixture = new Fixture();
        _roomService = new RoomService(_roomRepositoryMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async void InsertOne_ShouldInsertRoom_ValidObject()
    {
        // Arrange
        var room = _fixture.Create<Room>();

        _roomRepositoryMock
            .Setup(repo => repo.InsertOne(It.IsAny<Room>()))
            .ReturnsAsync(room.Id);
        _roomRepositoryMock
            .Setup(repo => repo.GetOne(room.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        // Act
        var returnedRoom = await _roomService.InsertOne(room, new CancellationToken());

        // Assert
        returnedRoom.Should().NotBeNull().And.Be(room);
    }

    [Fact]
    public async void InsertOne_ShouldThrowException_InvalidObject()
    {
        // Arrange
        var room = _fixture.Create<Room>();
        room.RoomNo = null;

        _roomRepositoryMock
            .Setup(repo => repo.InsertOne(It.IsAny<Room>()))
            .ReturnsAsync(room.Id);
        _roomRepositoryMock
            .Setup(repo => repo.GetOne(room.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        // Act
        var act = async () => await _roomService.InsertOne(room, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<Exception>();
    }
}