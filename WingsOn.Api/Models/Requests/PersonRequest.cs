using System;
using System.ComponentModel.DataAnnotations;
using WingsOn.Domain;

namespace WingsOn.Api.Models.Requests
{
    /// <summary>
    /// Object used to map the incoming request for a Passenger.
    /// This object works with the ModelState to verify the complete set of parameters sent.
    /// </summary>
    public class PersonRequest
    {
        public string Address { get; set; }

        public DateTime DateBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(GenderType))]
        public GenderType Gender { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
