using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Errors
{
    /// <summary>
    /// Validation error on forms
    /// </summary>
    public class ApiValidationError : ApiResponse
    {
        public ApiValidationError() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
