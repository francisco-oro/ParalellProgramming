 using Application.Tests;
 using Data;
 using Domain;
 using FluentAssertions;
 using Microsoft.EntityFrameworkCore;



 namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        private readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
            .UseInMemoryDatabase("Flights")
            .Options);

        private readonly BookingService bookingService;

        public FlightApplicationSpecifications()
        {
            bookingService = new BookingService(_entities: entities);
        }
        

        [Theory]
        [InlineData("m@m.com", 2)]
        [InlineData("a@a.com", 2)]
        public void Book_flights(string passengerEmail, int numberOfSeats)
        {


            var flight = new Flight(3);

            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail:passengerEmail, numberOfSeats:numberOfSeats ));

            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail: passengerEmail, numberOfSeats:numberOfSeats)
                );
        }

        [Theory]
        [InlineData(3)]
        [InlineData(10)]

        public void Frees_up_seats_after_booking(int initialCapacity)
        {
            // Given

            var flight = new Flight(initialCapacity);
            entities.Flights.Add(flight); 

            bookingService.Book(new BookDto(
                flightId: flight.Id, 
                passengerEmail:"m@m.com", 
                numberOfSeats:2));
            // When
            bookingService.CancelBooking(
                    new CancelBookingDto(flightId: flight.Id,
                        passengerEmail: "m@m.com", 
                        numberOfSeats: 2)
                    );
            // then
            bookingService.GetRemainingNumberOfSeatsFor(flight.Id).Should().Be(initialCapacity);
        }
    }


}