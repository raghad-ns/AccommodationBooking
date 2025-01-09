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

    async Task<Hotel> IHotelService.CreateHotel(Hotel hotel)
    {
        return await _hotelRepository.CreateHotel(hotel);
    }

    async Task IHotelService.DeleteHotelById(int hotelId)
    {
        await _hotelRepository.DeleteHotelById(hotelId);
    }

    async Task IHotelService.DeleteHotelByName(string name)
    {
        await _hotelRepository.DeleteHotelByName(name);
    }

    async Task<Hotel> IHotelService.GetHotelById(int id)
    {
        return await _hotelRepository.GetHotelById(id);
    }

    async Task<Hotel> IHotelService.GetHotelByName(string name)
    {
        return await _hotelRepository.GetHotelByName(name);
    }

    async Task<List<Hotel>> IHotelService.GetHotels(int page, int pageSize, HotelFilters hotelFilters)
    {
        return await _hotelRepository.GetHotels(page, pageSize, hotelFilters);
    }

    async Task<Hotel> IHotelService.UpdateHotel(int hotelId, Hotel hotel)
    {
        return await _hotelRepository.UpdateHotel(hotelId, hotel);
    }
}
