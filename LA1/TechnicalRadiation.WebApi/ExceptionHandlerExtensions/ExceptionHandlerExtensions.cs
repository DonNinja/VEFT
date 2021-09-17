using System;
using System.Net;
using TechnicalRadiation.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TechnicalRadiation.Models;

namespace TechnicalRadiation.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        if (exception is ResourceNotFoundException)
                        {
                            StatusCode = (int)HttpStatusCode.NotFound;
                        }
                        if (exception is ModelFormatException)
                        {
                            StatusCode = (int)HttpStatusCode.PreconditionFailed;
                        }
                        if (exception is ArgumentOutOfRangeException)
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        var exModel = new ExceptionModel
                        {
                            StatusCode = StatusCode,
                            ExceptionMessage = exception.Message,
                            StackTrace = exception.StackTrace
                        };

                        await context.Response.WriteAsync(exModel.ToString());

                    }

                });
            });
        }
    }
}