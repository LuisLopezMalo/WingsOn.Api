using System;
using System.ComponentModel.DataAnnotations;
using WingsOn.Domain;

namespace WingsOn.Api.Models.Requests
{
    /// <summary>
    /// Object used to map the incoming request to update just the Passenger info that can be updated.
    /// This object works with the ModelState to verify the complete set of parameters sent.
    /// </summary>
    public class UpdatablePersonRequest
    {
        public string Address { get; set; }

        public DateTime DateBirth { get; set; }
        
        [EnumDataType(typeof(GenderType))]
        public GenderType Gender { get; set; }

        public string Email { get; set; }
    }
}
