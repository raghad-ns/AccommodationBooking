using AccommodationBooking.Infrastructure.Cities.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Cities.Mappers;

[Mapper]
public partial class InfrastructureDomainCityMapper
{
    public partial Domain.Cities.Models.City ToDomainCity(City city);

    public partial City ToInfrastructureCity(Domain.Cities.Models.City city);
}