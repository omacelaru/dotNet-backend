using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotNet_backend.Data.Filters
{
    public class EmailVerifiedFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var emailVerifiedClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "EmailConfirmed")?.Value;
            bool isEmailVerified = emailVerifiedClaim == "True";

            if (!isEmailVerified)
            {
                context.Result = new BadRequestObjectResult("Email not verified");
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

}
