﻿using BancoApis.DomainServices.Abstractions.Requests;
using BancoApis.DomainServices.Abstractions.Responses;

namespace BancoApis.DomainServices.Dtos.Responses
{
    public class PagedResponse<T> : BasePagedRequest, IPagedResponse<T>
    {
        public bool Successed { get; private set; }
        public string Message { get; private set; }
        public List<string>? Errors { get; private set; }
        public T? Data { get; private set; }

        public PagedResponse()
        {
            
        }

        public PagedResponse(T data, int pageNumber, int pageSize, string message)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Successed = true;
            Message = message;
            Errors = null;
        }

        public PagedResponse(string message, T? data)
        {
            Successed = true;
            Message = message;
            Data = data;
        }

        public PagedResponse(string message, List<string>? errors = null)
        {
            Successed = false;
            Message = message;
            Errors = errors;
        }
    }
}
