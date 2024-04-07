using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Auth.Exceptions;

namespace Shared.Auth.Attributes;
public class ResultExceptionAttribute :Attribute , IExceptionFilter {
    public void OnException(ExceptionContext context) {
        if(context.Exception is AccountsException ex) {
            context.Result = new JsonResult(new { errors = ex.Errors });
            return;
        }
        if(context.Exception is Exception e) {
            context.Result = new JsonResult(new { error = e.Message });
            return;
        }
    }
}
