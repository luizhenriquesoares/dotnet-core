using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Infrastructure.Pipes
{
    public class JwtPipe: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.HttpContext.Request.Headers["Authorization"].Count == 0)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Unauthorized", statusCode = 401 });
                return;
            }
        }
            
    }
}
