using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eGym.MVC.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;


        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context/* other scoped dependencies */)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound ||
                    context.Response.StatusCode == (int)HttpStatusCode.GatewayTimeout)
                    context.Response.Redirect($"/error/{context.Response.StatusCode}");
            }
            /// Logging errore di validazione fluent
            catch (ValidationException e)
            {
                foreach (var err in e.Errors) _logger.LogInformation($"{err.PropertyName}: {err.ErrorCode} {err.Severity} | {err.ErrorMessage}", "ValidationException");
                await HandleErrorAsync(context, e);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogCritical($"{e}", "DbUpdateConcurrencyException");

                await HandleErrorAsync(context, e);
            }
            /// Logging errore di validazione record database
            catch (DbUpdateException e)
            {
                _logger.LogCritical($"{e}", "DbUpdateException");

                await HandleErrorAsync(context, e);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogCritical($"{e}", "FileNotFoundException");
                await HandleErrorAsync(context, e);
            }
            /// Logging errore generico
            catch (Exception e)
            {
                _logger.LogCritical($"{e}", "Exception");
                await HandleErrorAsync(context, e);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception e)
        {
            string responseJsonContent = "";
            HttpStatusCode responseStatusCode = HttpStatusCode.BadRequest;
            if (e is DbUpdateException || e is DbUpdateConcurrencyException) responseStatusCode = HttpStatusCode.InternalServerError;

#if (DEBUG)
            responseJsonContent = JsonConvert.SerializeObject(e, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseStatusCode;
            return context.Response.WriteAsync(responseJsonContent);
#elif (RELEASE)
            context.Response.Redirect($"/error/{((int)responseStatusCode)}");
            return Task.CompletedTask;
#endif

            object GetOnlySomeFieldsFromExceptionObject(Exception exc)
            {
                if (exc == null) return null;
                // Se l'eccezione che sto provando a serializzare non è una Validation
                // allora vado a ricercare una ValidationException nella InnerException
                if (!(exc is ValidationException)) return GetOnlySomeFieldsFromExceptionObject(exc.InnerException);
                return new
                {
                    exc.Message,
                    exc.Data,
                    InnerException = GetOnlySomeFieldsFromExceptionObject(exc.InnerException),
                    exc.HelpLink,
                    exc.HResult
                };
            }
        }
    }
}
