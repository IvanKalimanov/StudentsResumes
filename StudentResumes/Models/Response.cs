using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Core.Models
{
    public class Response<TData>
    {
        public int StatusCode { get; set; } = 200;

        public TData Data { get; set; } = default;

        public string Message { get; set; } = null;

        public Response(TData item)
        {
            Data = item;
            StatusCode = 200;
        }

        public Response(int code, string error)
        {
            StatusCode = code;
            Message = error;
        }

        public bool Succeeded()
        {
            return StatusCode == 200 && Message == null;
        }
    }
}
