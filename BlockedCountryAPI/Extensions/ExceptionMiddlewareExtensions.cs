using BlockedCountryAPI.Entities.ErrorModel;
using BlockedCountryAPI.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BlockedCountryAPI.Extensions;

public static class ExceptionMiddlewareExtensions
{
    private static int GetResponseStatusCode(Exception exception)
       => exception switch
       {
           InvalidCountryCodeException => StatusCodes.Status422UnprocessableEntity,
           AlreadyBlockedException => StatusCodes.Status409Conflict,
           _ => StatusCodes.Status500InternalServerError,
       };

    public static void AddConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError => appError.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature is not null)
            {
                context.Response.StatusCode = GetResponseStatusCode(contextFeature.Error);

                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = contextFeature.Error.Message
                }.ToString());
            }
        }
        ));
    }
}