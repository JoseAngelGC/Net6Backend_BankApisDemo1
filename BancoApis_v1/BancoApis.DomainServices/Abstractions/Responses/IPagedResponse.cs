namespace BancoApis.DomainServices.Abstractions.Responses
{
    public interface IPagedResponse<T>
    {
        public bool Successed { get; }
        public string Message { get; }
        public List<string>? Errors { get; }
        public T? Data { get; }
    }
}
