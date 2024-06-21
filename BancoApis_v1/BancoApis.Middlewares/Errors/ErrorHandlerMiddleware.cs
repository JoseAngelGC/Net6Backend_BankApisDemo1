using BancoApis.ApplicationServices.Mediator.Exceptions;
using BancoApis.DomainServices.Dtos.Responses;
using BancoApis.Middlewares.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BancoApis.Middlewares.Errors
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "Application/json";
                var responseModel = new ResultResponse<string>();

                switch (error)
                {
                    case ApiException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var apiExceptionResponse = new ResultResponse<string>(error.Message);
                        responseModel = apiExceptionResponse;
                        break;
                    case ValidationsException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var validationsExceptionResponse = new ResultResponse<string>(error.Message, e.errors);
                        responseModel = validationsExceptionResponse;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        var keyNotFoundExceptionResponse = new ResultResponse<string>(error.Message);
                        responseModel = keyNotFoundExceptionResponse;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var unHaddlerResponse = new ResultResponse<string>(error.Message);
                        responseModel = unHaddlerResponse;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
