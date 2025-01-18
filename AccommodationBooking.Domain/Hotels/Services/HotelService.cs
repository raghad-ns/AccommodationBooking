using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Repositories;

namespace AccommodationBooking.Domain.Hotels.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    async Task<Hotel> IHotelService.InsertOne(Hotel hotel, CancellationToken cancellationToken)
    {
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
        return _hotelRepository.UpdateOne(hotelId, hotel);
    }
}