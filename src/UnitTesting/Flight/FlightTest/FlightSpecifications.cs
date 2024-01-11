using Domain;
using FluentAssertions;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            var flight = new Flight(seatCapacity: 3);

            flight.Book("jannick@phionira.com", 1);

            flight.RemainingNumberOfSeats.Should().Be(2);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            // Given
            var flight = new Flight(3);
            // When
            var error = flight.Book("francisco@phionira.com", 4);
            // Then
            error.Should().BeOfType<OverbookingError>();
        }
    }
}