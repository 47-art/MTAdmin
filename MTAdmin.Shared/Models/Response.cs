using System;
using System.Collections.Generic;

namespace MTAdmin.Shared.Models
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T value)
        {
            Data = value;
        }

        public T Data { get; set; }
        public Exception Ex { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = true;

        protected internal Response(T value, bool isSuccess, string message) : this(value)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        protected internal Response(Exception ex, string message = "", T value = default) : this(value)
        {
            Message = message;
            Ex = ex;
            IsSuccess = false;
        }

        public static Response<T> Success(T data, string message = "")
        {
            return new Response<T>(data, true, message);
        }

        public static Response<T> Error(Exception ex, T data = default, string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                message = ex.Message;
            }
            return new Response<T>(ex, message, data);
        }

        public static Response<PagedList<T>> ToPagedResponse(IReadOnlyList<T> items, int count, int pageNumber, int pageSize, string message = "")
        {
            PagedList<T> pagedList = new PagedList<T>(items, count, pageNumber, pageSize);
            return new Response<PagedList<T>>(pagedList, true, message);
        }
    }
}
