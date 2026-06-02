using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is MyRecipeBookException)            
                HandleProjectException(context);           
            else           
                UnknownProjectException(context);           
        }

        private static void HandleProjectException(ExceptionContext context)
        {
            if(context.Exception is ErrorOnValidateException)
            {
                var exception = context.Exception as ErrorOnValidateException;

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorsJson(exception.ErrorMessage));
            }

        }

        private static void UnknownProjectException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new BadRequestObjectResult(new ResponseErrorsJson(ResourceExceptionsMessage.ERRO_DESCONHECIDO));

        }
            


    }
}
