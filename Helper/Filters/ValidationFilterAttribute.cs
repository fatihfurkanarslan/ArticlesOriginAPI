using Helper.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly ILoggerManager logger;
        public ValidationFilterAttribute(ILoggerManager _logger)
        {
            //logger = _logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param == null)
            {
               // logger.LogError($"Object sent from client is null. Controller: {controller}, Action: {action}");
                context.Result = new BadRequestObjectResult($"Object is null.  Controller: {controller}, Action: {action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                //logger.LogError($"Invalid model state for the object. Controller: {controller}, Action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
