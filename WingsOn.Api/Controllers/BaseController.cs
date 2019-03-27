using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    /// <summary>
    /// Base controller class to keep shared properties as repository or log object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
        where T : DomainObject
    {
        /// <summary>
        /// Main repository object.
        /// </summary>
        protected readonly IRepository<T> repository;

        /// <summary>
        /// Logging.
        /// </summary>
        protected ILogger<T> logger { get; set; }

        /// <summary>
        /// Receives the injected Repository.
        /// </summary>
        /// <param name="repository"></param>
        public BaseController(IRepository<T> repository)
        {
            this.repository = repository;
            // TODO: Here the logger should be injected.
            //this.logger = logger;
        }
    }
}