using System.Collections.Generic;
using System.Linq;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkoutPlanner.API.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(ICollection<Error> errors)
        {
            if (errors is not null)
            {
                if (errors.Count is 0)
                {
                    return Problem();
                }

                if (errors.All(error => error.Type == ErrorType.Validation))
                {
                    return ValidationProblem(errors);
                }

                HttpContext.Items["errors"] = errors;
            }

            return Problem(errors.First());
        }

        private IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Failure => StatusCodes.Status403Forbidden,
                ErrorType.Unexpected => StatusCodes.Status501NotImplemented,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private IActionResult ValidationProblem(ICollection<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }

    }
}