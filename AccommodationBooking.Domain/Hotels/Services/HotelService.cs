using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Repositories;
using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Library.Pagination.Models;
using FluentValidation;

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

    public async Task<Hotel> InsertOne(Hotel hotel, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(hotel);
        var id = await _hotelRepository.InsertOne(hotel);
        return await GetOne(id, cancellationToken);
    }

    public Task DeleteOne(int hotelId)
    {
        return _hotelRepository.DeleteOne(hotelId);
    }

    public Task<Hotel> GetOne(int id, CancellationToken cancellationToken)
    {
        return _hotelRepository.GetOne(id, cancellationToken);
    }


    public async Task<List<Room>> GetRooms(int id, CancellationToken cancellationToken)
    {
        return await _hotelRepository.GetRooms(id, cancellationToken);
    }

    public Task<Hotel> GetOneByName(string name, CancellationToken cancellationToken)
    {
        return _hotelRepository.GetOneByName(name, cancellationToken);
    }

    public Task<PaginatedData<Hotel>> Search(int page, int pageSize, HotelFilters hotelFilters, CancellationToken cancellationToken)
    {
        return _hotelRepository.Search(page, pageSize, hotelFilters, cancellationToken);
    }

    public Task<Hotel> UpdateOne(int hotelId, Hotel hotel)
    {
        _validator.ValidateAndThrow(hotel);
        return _hotelRepository.UpdateOne(hotelId, hotel);
    }
}