using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Domain.Hotels.Repositories;

public interface IHotelRepository
{
    Task<List<Hotel>> GetHotels(int page, int pageSize, HotelFilters hotelFilters);
    Task<Hotel> GetHotelById(int id);
    Task<Hotel> GetHotelByName(string name);
    Task<Hotel> CreateHotel(Hotel hotel);
    Task<Hotel> UpdateHotel(int hotelId, Hotel hotel);
    Task DeleteHotelById(int hotelId);
    Task DeleteHotelByName(string name);
}
