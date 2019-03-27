using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Api.Extensions;
using WingsOn.Api.Models.Responses;
using WingsOn.Api.Models;
using System;
using WingsOn.Api.Models.Requests;

namespace WingsOn.Api.Controllers
{
    /// <summary>
    /// Api for managing all the bookings functionality.
    /// </summary>
    public class BookingsController : BaseController<Booking>
    {
        public BookingsController(IRepository<Booking> repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Get all bookings from the repository.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            IEnumerable<Booking> bookings = repository.GetAll();
            if (bookings.Count() == 0)
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND, "No bookings found"));

            return Ok(ResponseObject.Ok(bookings));
        }

        /// <summary>
        /// Get a single booking from the repository.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetBookingSingle")]
        public IActionResult Get(int id)
        {
            Booking booking = repository.Get(id);
            if (booking.IsNull())
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND, string.Format("No booking found for id: {0}", id)));

            return Ok(ResponseObject.Ok(booking));
        }

        /// <summary>
        /// *****************
        /// TASK 4 - Endpoint that creates a booking from an existing flight for a new passenger.
        /// *****************
        /// Creates a new Booking from an existing flight while creating a new passenger and
        /// relating it to the booking.
        /// Checks for valid Flight and for non-existing passenger.
        /// </summary>
        /// <param name="flightNumber">The number of the flight.</param>
        /// <param name="bookingPassenger">The <see cref="PersonRequest"/> object from the request.</param>
        /// <param name="personRepository">The injected person repository.</param>
        /// <param name="flightRepository">The injected flight repository.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public IActionResult Post([FromBody] BookingPersonRequest bookingPassenger,
            [FromServices] IRepository<Person> personRepository,
            [FromServices] IRepository<Flight> flightRepository)
        {
            // Check for flight existence.
            Flight flight = flightRepository.GetAll().FirstOrDefault(f => f.Number.Trim() == bookingPassenger.FlightNumber.Trim());

            if (flight.IsNull())
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND,
                    string.Format("No flight found with number: {0}", bookingPassenger.FlightNumber)));

            // Check if a passenger already has the same email, this should not happen as this is supposed to be a new user.
            Person existingPassenger = personRepository.GetAll().FirstOrDefault(
                p => p.Email.Equals(bookingPassenger.Passenger.Email, StringComparison.OrdinalIgnoreCase));
            if (!existingPassenger.IsNull())
                return BadRequest(ResponseObject.StatusCode(MessageCode.ERROR_DUPLICATE_ENTRY,
                    string.Format("Passenger already found with email: {0}", existingPassenger.Email)));


            // ----- Create new passenger.
            // This can be done with a mapping library or if wanted to be done manually, could be done with reflection to be dynamic.
            Person newPassenger = new Person()
            {
                Name = bookingPassenger.Passenger.Name,
                Email = bookingPassenger.Passenger.Email,
                Address = bookingPassenger.Passenger.Address,
                DateBirth = bookingPassenger.Passenger.DateBirth,
                Gender = bookingPassenger.Passenger.Gender,
                Id = personRepository.GetAll().Max(b => b.Id) + 1 // Simulating just an auto-increment from DB.
            };

            // - Using an ORM like NHibernate or Entity Framework the saving for the person should be fired in cascade when 
            //      the saving for the booking is made.
            personRepository.Save(newPassenger);


            // --- Creates the new booking with the sent flight number and the new passenger.
            int id = repository.GetAll().Max(b => b.Id) + 1; // This depends on how it is being generated, maybe auto-increment in DB.

            // Just adding +1 to the last book number and check if it is unique, this algorithm could be anything.
            // Depending on any number of variables... type of flight, plane, departure, destination, data from passenger, etc.
            // I think this is a propietary algorithm or maybe dictated by an ISO.
            string number = string.Empty;
            do
            {
                int bookNumber = repository.GetAll().Max(b => int.Parse(b.Number.Substring(3))) + 1;
                number = "WO-" + bookNumber;
            }
            while (repository.GetAll().Any(r => r.Number == number));

            IEnumerable<Person> passengers = new[] { newPassenger }; // As it is a new booking this is the first and only passenger.

            Booking booking = new Booking()
            {
                Id = id,
                Customer = newPassenger,
                Flight = flight,
                Passengers = passengers,
                Number = number,
                DateBooking = DateTime.Now
            };

            // Save the new booking.
            repository.Save(booking);

            return CreatedAtAction("Get", ResponseObject.Ok(booking));
        }
    }
}