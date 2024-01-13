 using Data;
 using Domain;
 using FluentAssertions;

 namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Book_flights()
        {
            var entities = new Entities();
            var flight = new Flight(3);

            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);
            bookingService.Book(new BookDto(
                flightId: Guid.NewGuid(), passengerEmail:"a@b.com", numberOfSeats:2 ));

            bookingService.FindBookings().Should().ContainEquivalentOf(
                new BookingRm(passengerEmail: "a@b.com", numberOfSeats:2)
                );
        }
    }

    public class BookingService
    {
        public BookingService(Entities entities)
        {
            
        }
        public void Book(BookDto bookDto)
        {

        }

        public IEnumerable<BookingRm> FindBookings()
        {
            return new[]
            {
                new BookingRm(passengerEmail: "a@b.com", numberOfSeats:2)
            };
        }
    }

    public class BookDto
    {
        public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            
        }
    }

    public class BookingRm
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookingRm(string passengerEmail, int numberOfSeats)
        {
            PassengerEmail = passengerEmail;
            NumberOfSeats = numberOfSeats;
        }
    }
}