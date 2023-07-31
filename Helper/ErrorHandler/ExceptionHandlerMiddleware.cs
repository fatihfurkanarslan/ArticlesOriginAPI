using Entities.ErrorModel;
using Helper.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper.ErrorHandler
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            //adds a middleware to the pipeline that will catch exception and log them, and re-execute request in an another pipeline.
            app.UseExceptionHandler(appError =>
            //adds a TERMINAL middleware delegate to the application's request. Our delegate is context
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    //logger.LogError($"Something went wrong {contextFeature.Error}.");

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = "Internal Server Error Occured!!."
                    }.ToString());
                }
            }
            ));


        }
    }
}
