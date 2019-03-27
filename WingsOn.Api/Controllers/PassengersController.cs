using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Api.Models.Requests;
using WingsOn.Api.Models.Responses;
using WingsOn.Api.Extensions;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    /// <summary>
    /// Api for managing all the passengers functionality.
    /// </summary>
    public class PassengersController : BaseController<Person>
    {
        public PassengersController(IRepository<Person> repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Get all passengers from the repository.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Person> passengers = repository.GetAll();
            if (passengers.Count() == 0)
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND, "No passengers found"));

            return Ok(ResponseObject.Ok(passengers));
        }

        /// <summary>
        /// *****************
        /// TASK 1
        /// *****************
        /// Get a single passenger from the repository by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Person passenger = repository.Get(id);
            if (passenger.IsNull())
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND, string.Format("No passenger found for id: {0}", id)));

            return Ok(ResponseObject.Ok(passenger));
        }

        /// <summary>
        /// *****************
        /// TASK 5 - Endpoint that lists all the male passengers.
        ///     By default calling the api/gender endpoint returns male passengers.
        ///     Also can be called with parameters:
        ///         Calling male with api/gender/male endpoint.
        ///         Calling female with api/gender/female endpoint.
        /// *****************
        /// Get all the passengers by gender.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("gender/{gender=0}")]
        public IActionResult GetByGender(GenderType gender)
        {
            IEnumerable<Person> passengers = repository.GetAll().Where(p => p.Gender == gender);
            if (passengers.Count() == 0)
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND, string.Format("No passengers found for gender: {0}", gender.ToString())));

            return Ok(ResponseObject.Ok(passengers));
        }

        /// <summary>
        /// *****************
        /// TASK 3 - Endpoint that updates a person's email address.
        ///     It can be extended to update all the properties.
        /// Use of Patch HTTP method for partial updates.
        /// *****************
        /// </summary>
        /// <param name="id">Id of the passenger.</param>
        /// <param name="passenger">The passenger information to be updated.</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatablePersonRequest passenger)
        {
            // Check for passenger existence.
            Person existingPassenger = repository.Get(id);
            if (existingPassenger.IsNull())
                return NotFound(ResponseObject.StatusCode(MessageCode.ERROR_VALUE_NOT_FOUND, string.Format("No passenger found for id: {0}", id)));

            existingPassenger.Email = passenger.Email;

            repository.Save(existingPassenger);

            return Ok(ResponseObject.Ok(existingPassenger));
        }
    }
}
