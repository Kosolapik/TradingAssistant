using Binance.Net.Clients;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace TradingAssistant.Infrastructure.Exchanges.Binance
{
    public interface IBinanceClient : IExchangeClient<BinanceRestClient>
    {
      
    }
}
