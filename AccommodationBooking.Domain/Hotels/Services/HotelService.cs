using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Repositories;
using FluentValidation;
using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Domain.Hotels.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<Hotel> _validator;

    public HotelService(IHotelRepository hotelRepository, IValidator<Hotel> validator)
    {
        _hotelRepository = hotelRepository;
        _validator = validator;
    }

    async Task<Hotel> IHotelService.InsertOne(Hotel hotel, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(hotel);
        var id = await _hotelRepository.InsertOne(hotel);
        return await _hotelRepository.GetOne(id, cancellationToken);
    }

    Task IHotelService.DeleteOne(int hotelId)
    {
        return _hotelRepository.DeleteOne(hotelId);
    }

    Task<Hotel> IHotelService.GetOne(int id, CancellationToken cancellationToken)
    {
        return _hotelRepository.GetOne(id, cancellationToken);
    }


    async Task<List<Room>> IHotelService.GetRooms(int id, CancellationToken cancellationToken)
    {
        return await _hotelRepository.GetRooms(id, cancellationToken);
    }

    Task<Hotel> IHotelService.GetOneByName(string name, CancellationToken cancellationToken)
    {
        return _hotelRepository.GetOneByName(name, cancellationToken);
    }

    Task<PaginatedData<Hotel>> IHotelService.Search(int page, int pageSize, HotelFilters hotelFilters, CancellationToken cancellationToken)
    {
        return _hotelRepository.Search(page, pageSize, hotelFilters, cancellationToken);
    }

    Task<Hotel> IHotelService.UpdateOne(int hotelId, Hotel hotel)
    {
        _validator.ValidateAndThrow(hotel);
        return _hotelRepository.UpdateOne(hotelId, hotel);
    }
}