using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Domain.Hotels.Repositories;

public interface IHotelRepository
{
    Task<PaginatedData<Hotel>> Search(int page, int pageSize, HotelFilters hotelFilters);
    Task<Hotel> GetOne(int id);
    Task<List<Room>> GetRooms(int id);
    Task<Hotel> GetOneByName(string name);
    Task<Hotel> AddOne(Hotel hotel);
    Task<Hotel> UpdateOne(int hotelId, Hotel hotel);
    Task DeleteOne(int hotelId);
}