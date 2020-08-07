using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }



        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //Logger.Log.LogError(ex.Message + ex.StackTrace);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //var response = new ResponseModel((int)HttpStatusCode.InternalServerError, "Internal server error");
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //context.Response.ContentType = "application/json";

            //if (exception.GetType() == typeof(UserNotAllowedException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //    response = new ResponseModel((int)HttpStatusCode.Forbidden, "Not enough authority");
            //}


            //if (exception.GetType() == typeof(UserNotFoundException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //    response = new ResponseModel((int)HttpStatusCode.NotFound, "There is no such user");
            //}

            //if (exception.GetType() == typeof(VideoNotFoundException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //    response = new ResponseModel((int)HttpStatusCode.NotFound, "There is no such video");
            //}


            //if (exception.GetType() == typeof(InvalidPasswordException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //    response = new ResponseModel((int)HttpStatusCode.Forbidden, "Invalid Password");
            //}

            //if (exception.GetType() == typeof(ReachedVideoLimitException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.OK;
            //    response = new ResponseModel((int)HttpStatusCode.OK, "Reached Video Limit");
            //}

            //if (exception.GetType() == typeof(IOException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //    response = new ResponseModel((int)HttpStatusCode.NotFound, "Something went wrong");
            //}


            //if (exception.GetType() == typeof(IncorrectFragmentIntervalException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    response = new ResponseModel((int)HttpStatusCode.BadRequest, "Incorrect Fragment Interval");
            //}


            //if (exception.GetType() == typeof(UserAlreadyExistsException))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.OK;
            //    response = new ResponseModel((int)HttpStatusCode.OK, "User already exists");
            //}

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
