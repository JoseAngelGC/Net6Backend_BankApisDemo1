using BancoApis.DomainServices.Abstractions.Responses;

namespace BancoApis.DomainServices.Dtos.Responses
{
    public class ResultResponse<T> : IResultResponse<T>
    {
        public bool Successed { get; private set; }
        public string Message { get; private set; }
        public List<string>? Errors { get; private set; }
        public T? Data { get; private set; }

        public ResultResponse()
        {
            
        }

        public ResultResponse(string message, T? data)
        {
            Successed = true;
            Message = message;
            Data = data;
        }

        public ResultResponse(string message, List<string>? errors = null)
        {
            Successed = false;
            Message = message;
            Errors = errors;
        }
    }
}
