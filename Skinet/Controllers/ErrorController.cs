using Microsoft.AspNetCore.Mvc;
using Skinet.Errors;

namespace Skinet.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] // ignore for swagger
    public class ErrorController : BaseApiController
    {
        /// <summary>
        /// Handling any type of HTTP method, hence no http annotation
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
