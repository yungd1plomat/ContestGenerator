using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ContestGenerator.Helpers
{
    public class ForwardedHostAttribute : Attribute, IActionConstraint
    {
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Headers.ContainsKey("X-Forwarded-Host");
        }
    }
}
