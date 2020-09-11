using Itopya.Application.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Itopya.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature.Error is HttpException httpException)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = httpException.StatusCode; 
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(contextFeature.Error.Message));
                    }
                    else if(contextFeature != null)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject("Internal Server Error"));
                    }
                });
            });
        }
    }
}