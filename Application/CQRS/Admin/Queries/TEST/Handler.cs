using CryptoClients.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using MediatR;
using TradingAssistant.Application.DTOs;
using TradingAssistant.Infrastructure.Exchanges.Crypto;

namespace TradingAssistant.Application.CQRS.Admin.Queries.TEST;

public class Handler : IRequestHandler<Query, IEnumerable<IAssetsRestClient>>
{
    private readonly ICryptoClient _client;

    public Handler(
        ICryptoClient client
    ){
        _client = client;
    }

    public async Task<IEnumerable<IAssetsRestClient>> Handle(
        Query query,
        CancellationToken cancellationToken)
    {
        var result = _client.cl().GetAssetsClients().Where(e => e.Authenticated == true);

        return result;

       
    }
}