namespace BancoApis.DomainServices.Abstractions.Commons
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
