using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Domain.Hotels.Services;

public interface IHotelService
{
    Task<List<Hotel>> Search(int page, int pageSize, HotelFilters hotelFilters);
    Task<Hotel> GetOne(int id);
    Task<Hotel> GetOneByName(string name);
    Task<Hotel> AddOne(Hotel hotel);
    Task<Hotel> UpdateOne(int hotelId, Hotel hotel);
    Task DeleteOne(int hotelId);
}