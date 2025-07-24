

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    public interface ICryptoClient
    {
        Task<Dictionary<string, List<object>>> GetSpotSymbolsAsync(string exchange);
    }
}
