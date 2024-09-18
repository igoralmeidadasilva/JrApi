using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Core.Interfaces;

public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
    where TRequest : ICommand<TResponse>
    where TResponse : IResult
{
    
}