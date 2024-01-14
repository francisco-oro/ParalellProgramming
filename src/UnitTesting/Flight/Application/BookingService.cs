using Data; 
namespace Application
{
    public class BookingService
    {
        public Entities entities { get; set; }
        public BookingService(Entities _entities)
        {
            entities = _entities;
        }
        public void Book(BookDto bookDto)
        {
            var flight = entities.Flights.Find(bookDto.FlightId);
            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            entities.SaveChanges();
        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return entities.Flights
                .Find(flightId)
                .BookingList
                .Select(booking => new BookingRm(
                    booking.PassengerEmail,
                    booking.NumberOfSeats));
        }

        public void CancelBooking(CancelBookingDto cancelBookingDto)
        {
            var flight = entities.Flights.Find(cancelBookingDto.FlightId);
            flight.CancelBooking(cancelBookingDto.PassengerEmail, cancelBookingDto.NumberOfSeats);
            entities.SaveChanges(); 
        }

        public int GetRemainingNumberOfSeatsFor(Guid id)
        {
            return entities.Flights.Find(id).RemainingNumberOfSeats;
        }
    }
}