using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccommodationBooking.Domain.Users.Exceptions;

public class UserAlreadyExistedException() : Exception("Username or email already existed");