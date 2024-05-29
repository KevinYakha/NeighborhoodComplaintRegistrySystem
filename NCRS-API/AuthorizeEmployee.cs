using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NCRS_API;

public class AuthorizeEmployee : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers["Token"] == "EmployeeToken")
        {
        }
        else
        {
            context.Result = new UnauthorizedResult();

            string token = context.HttpContext.Request.Headers["Token"];
            string action = context.RouteData.Values["Action"].ToString();
            Console.WriteLine($"Connection with token {token} tried to access {action}.");

            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        string employeeToken = context.HttpContext.Request.Headers["Token"];
        string accessedAction = context.RouteData.Values["Action"].ToString();
        Console.WriteLine($"Employee with token {employeeToken} has accessed {accessedAction}.");
    }
}
