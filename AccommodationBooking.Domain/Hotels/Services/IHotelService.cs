using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Domain.Hotels.Services;

public interface IHotelService
{
    Task<PaginatedData<Hotel>> Search(int page, int pageSize, HotelFilters hotelFilters, CancellationToken cancellationToken);
    Task<Hotel> GetOne(int id, CancellationToken cancellationToken);
    Task<List<Room>> GetRooms(int id, CancellationToken cancellationToken);
    Task<Hotel> GetOneByName(string name, CancellationToken cancellationToken);
    Task<Hotel> InsertOne(Hotel hotel, CancellationToken cancellationToken);
    Task<Hotel> UpdateOne(int hotelId, Hotel hotel);
    Task<int> DeleteOne(int hotelId);
}