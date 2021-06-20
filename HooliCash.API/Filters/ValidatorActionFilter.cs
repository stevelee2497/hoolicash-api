using HooliCash.Shared;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HooliCash.API.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                throw new HooliCashException(filterContext.ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
