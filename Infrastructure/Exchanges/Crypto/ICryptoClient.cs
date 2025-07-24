using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    public interface ICryptoClient
    {
        Task<Dictionary<string, List<object>>> GetSpotSymbolsAsync(string exchange);
    }
}
