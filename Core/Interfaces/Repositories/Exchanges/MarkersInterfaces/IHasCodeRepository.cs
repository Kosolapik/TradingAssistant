namespace TradingAssistant.Core.Interfaces.Repositories.Exchanges.MarkersInterfaces
{
    public interface IHasCodeRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
    }
}
