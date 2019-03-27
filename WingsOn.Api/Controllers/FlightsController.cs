using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models.Responses;
using WingsOn.Api.Extensions;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    /// <summary>
    /// Api for managing all the flights functionality.
    /// </summary>
    public class FlightsController : BaseController<Flight>
    {
        public FlightsController(IRepository<Flight> repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Get all the flights from the repository.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Flight> flights = repository.GetAll();
            if (flights.Count() == 0)
                return NotFound(ResponseObject.StatusCode(Models.MessageCode.ERROR_VALUE_NOT_FOUND, "No flights found"));

            return Ok(ResponseObject.Ok(flights));
        }

        /// <summary>
        /// Get a single flight from the repository by its flightNumber.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{flightNumber}")]
        public IActionResult Get(string flightNumber)
        {
            // Asumes the flight numbers are case sensitive.
            Flight flight = repository.GetAll().FirstOrDefault(f => f.Number.Trim() == flightNumber.Trim());

            if (flight.IsNull())
                return NotFound(ResponseObject.StatusCode(Models.MessageCode.ERROR_VALUE_NOT_FOUND,
                    string.Format("No flight found with number: {0}", flightNumber)));

            return Ok(ResponseObject.Ok(flight));
        }

        /// <summary>
        /// *****************
        /// TASK 2
        /// *****************
        /// Get the passengers that are booked to a specific flight.
        /// </summary>
        /// <param name="flightNumber">The number of the flight.</param>
        /// <param name="bookingRepository">The <see cref="IRepository"/> repository injected for bookings</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{flightNumber}/passengers")]
        public IActionResult GetPassengersByFlight(string flightNumber, [FromServices] IRepository<Booking> bookingRepository)
        {
            // --- All the solution can be made using Linq. This is a solution design that must be decided before the implementation
            // or when making a refactoring.
            // This method could be achieved by the next query.
            /*
            IList<Person> passengers = (
                from fl in repository.GetAll()
                join bo in bookingRepository.GetAll() on fl.Number.Trim() equals bo.Flight.Number.Trim()
                where fl.Number.Trim() == flightNumber.Trim()
                select bo.Passengers
                ).SelectMany(b => b).ToList();
            */

            // Check for flight existence.
            Flight flight = repository.GetAll().FirstOrDefault(f => f.Number.Trim() == flightNumber.Trim());

            if (flight.IsNull())
                return NotFound(ResponseObject.StatusCode(Models.MessageCode.ERROR_VALUE_NOT_FOUND,
                    string.Format("No flight found with number: {0}", flightNumber)));

            // Get the booked passengers for the flight.
            IList<Person> passengers = bookingRepository.GetAll().Where(b => b.Flight.Number.Trim() == flightNumber.Trim())
                .SelectMany(b => b.Passengers).ToList();

            if (passengers.Count() == 0)
                return NotFound(ResponseObject.StatusCode(Models.MessageCode.ERROR_VALUE_NOT_FOUND,
                    string.Format("Passengers not for flight found with number: {0}", flightNumber)));

            return Ok(ResponseObject.Ok(passengers));
        }
    }
}