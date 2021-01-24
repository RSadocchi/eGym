using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System;
using eGym.Core.Security;

namespace eGym.Core.Log
{

    public class TelemetryGDPR
    {
        readonly RequestDelegate _next;
        readonly ILogger<TelemetryGDPR> _logger;
        readonly RecyclableMemoryStreamManager _memoryStreamManager;

        public TelemetryGDPR(
            RequestDelegate requestDelegate,
            ILogger<TelemetryGDPR> logger)
        {
            _next = requestDelegate;
            _logger = logger;
            _memoryStreamManager = new RecyclableMemoryStreamManager();
        }


        public async Task Invoke(
            HttpContext context,
            ILogRepository logRepository)
        {
            var endpointIn = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attributeIn = endpointIn?.Metadata.GetMetadata<GDPRAttribute>();

            if (attributeIn == null) await _next(context);
            else
            {
                var entity = new Log_GDPR()
                {
                    IPAddress = context.Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = context.Request.Headers["User-Agent"].ToString(),
                    User = context.Request.HttpContext.User.Identity.Name
                };
                
                this.LogRequest(context: context, ref entity);
                this.LogResponse(context: context, ref entity);

                await logRepository.GDPR_SaveAsync(entity: entity, saveChanges: true);
            }
        }

        private void LogRequest(HttpContext context, ref Log_GDPR entity)
        {
            context.Request.EnableBuffering();
            using (var requestStream = _memoryStreamManager.GetStream())
            {
                context.Request.Body.CopyToAsync(requestStream).Wait();
                var headers = string.Join(",", context.Request.Headers.ToDictionary(d => d.Key, d => d.Value));
                var cookies = string.Join(",", context.Request.Cookies.ToDictionary(d => d.Key, d => d.Value));
                var body = ReadStreamInChunks(requestStream);
                
                entity.RequestUrl = $"{context.Request.Scheme}/{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
                entity.RequestCookies = cookies;
                entity.RequestHeaders = headers;
                entity.RequestBody = body;

                _logger.Log(
                    LogLevel.Information,
                    $"REQUEST:{Environment.NewLine}" +
                    $" URL: '{entity.RequestUrl}' (Protocol '{context.Request.Protocol}', Method '{context.Request.Method}'){Environment.NewLine}" + 
                    $" COOKIES: {entity.RequestCookies}{Environment.NewLine}" + 
                    $" HEADERS: {entity.RequestHeaders}{Environment.NewLine}" + 
                    $" BODY: {entity.RequestBody}{Environment.NewLine}" + 
                    $"-----------------------------");
            }
        }

        private void LogResponse(HttpContext context, ref Log_GDPR entity)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = _memoryStreamManager.GetStream())
            {
                context.Response.Body = responseBody;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                _next(context).Wait();
                stopwatch.Stop();
                
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                entity.ResponseBody = new StreamReader(context.Response.Body).ReadToEndAsync().Result;
                entity.ExecutionTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                entity.ResponseStatusCode = (HttpStatusCode)context.Response.StatusCode;

                _logger.Log(
                    LogLevel.Information,
                    $"RESPONSE:{Environment.NewLine}" +
                    $" REQUEST URL: '{entity.RequestUrl}' (Protocol '{context.Request.Protocol}', Method '{context.Request.Method}'){Environment.NewLine}" +
                    $" STATUS CODE: {context.Response.StatusCode} - {entity.ResponseStatusCode}{Environment.NewLine}" +
                    $" EXECUTION TIME: {entity.ExecutionTime}{Environment.NewLine}" +
                    $" BODY: {entity.ResponseBody}{Environment.NewLine}" +
                    $"-----------------------------");
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
