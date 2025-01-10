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

    async Task<Hotel> IHotelService.AddOne(Hotel hotel)
    {
        return await _hotelRepository.AddOne(hotel);
    }

    async Task IHotelService.DeleteOne(int hotelId)
    {
        await _hotelRepository.DeleteOne(hotelId);
    }

    async Task<Hotel> IHotelService.GetOne(int id)
    {
        return await _hotelRepository.GetOne(id);
    }

    async Task<Hotel> IHotelService.GetOneByName(string name)
    {
        return await _hotelRepository.GetOneByName(name);
    }

    async Task<List<Hotel>> IHotelService.Search(int page, int pageSize, HotelFilters hotelFilters)
    {
        return await _hotelRepository.Search(page, pageSize, hotelFilters);
    }

    async Task<Hotel> IHotelService.UpdateOne(int hotelId, Hotel hotel)
    {
        return await _hotelRepository.UpdateOne(hotelId, hotel);
    }
}