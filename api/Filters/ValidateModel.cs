using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Filters;

public class ValidateModel : ActionFilterAttribute
{
     
    /*
     * checks if the model state is valid.
     * If it is valid, the method returns without doing anything.
     * If the model state is not valid, it retrieves the error messages from the model state.
     * It then aggregates these error messages into a single string.  
     */
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;
        var errorMessages = context.ModelState
            .Values
            .SelectMany(i => i.Errors.Select(e => e.ErrorMessage))
            .Aggregate((i, j) => i + "," + j);
        context.Result = new JsonResult(new ResponseDto
        {
            MessageToClient = errorMessages
        })
        {
            StatusCode = 400
        };
    }
    
}