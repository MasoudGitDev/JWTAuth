using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Auth.Exceptions;
using Shared.Auth.Models;

namespace Shared.Auth.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class AccountResultExceptionAttribute : Attribute, IExceptionFilter {
    public void OnException(ExceptionContext context) {
        if(context.Exception is AccountException e) {
            context.Result = new JsonResult(new AccountResult(e.Errors));
            return;
        }
        if(context.Exception is Exception ex) {
            context.Result = new JsonResult(new AccountResult([new("Unknown" , ex.Message)]));
            return;
        }
    }
}
