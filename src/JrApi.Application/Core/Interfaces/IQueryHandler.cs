using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Core.Interfaces;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
    where TRequest : IQuery<TResponse>
    where TResponse : Result
{
    
}