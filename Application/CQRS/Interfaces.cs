using MediatR;

namespace TradingAssistant.Application.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
public interface ICommand<out TResponse> : IRequest<TResponse> { }
public interface IVoidCommand : IRequest { }  // Для void-команд