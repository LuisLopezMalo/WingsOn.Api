using System;
using System.ComponentModel.DataAnnotations;
using WingsOn.Domain;

namespace WingsOn.Api.Models.Requests
{
    /// <summary>
    /// Object used to map the incoming request for a Passenger that will be booked to a flight.
    /// This object works with the ModelState to verify the complete set of parameters sent.
    /// </summary>
    public class BookingPersonRequest
    {
        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public PersonRequest Passenger { get; set; }
    }
}
