using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WingsOn.Api.Models.Responses;

namespace WingsOn.Api.Filters
{
    /// <summary>
    /// Filter to intercept the requests and check for invalid Model States and send the corresponding message.
    /// This make the results of the api are consistent for valid or invalid model requests.
    /// </summary>
    public class ModelInvalidAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(ResponseObject.StatusCode(Models.MessageCode.ERROR_UNKNOWN, 
                    "Error processing request.", (context.Result as ObjectResult).Value));
            }

            base.OnResultExecuting(context);
        }
    }
}
