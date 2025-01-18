using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Domain.Hotels.Services;

public interface IHotelService
{
    Task<PaginatedData<Hotel>> Search(int page, int pageSize, HotelFilters hotelFilters, CancellationToken cancellationToken);
    Task<Hotel> GetOne(int id, CancellationToken cancellationToken);
    Task<Hotel> GetOneByName(string name, CancellationToken cancellationToken);
    Task<Hotel> InsertOne(Hotel hotel, CancellationToken cancellationToken);
    Task<Hotel> UpdateOne(int hotelId, Hotel hotel);
    Task DeleteOne(int hotelId);
}