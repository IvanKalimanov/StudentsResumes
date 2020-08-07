using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentResumes.Core.Exceptions;
using StudentResumes.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            var response = new Response<int>((int)HttpStatusCode.InternalServerError, "Internal server error");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            if (exception.GetType() == typeof(DbUpdateException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new Response<int>((int)HttpStatusCode.NotFound, "Error while updating database. Check data you sent");
            }
            
            if (exception.GetType() == typeof(EntityNotFoundException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new Response<int>((int)HttpStatusCode.NotFound, "Entity does not exist");
            }

            if (exception.GetType() == typeof(IOException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new Response<int>((int)HttpStatusCode.NotFound, "Something went wrong");
            }



            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
