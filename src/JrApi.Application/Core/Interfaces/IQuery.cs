using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Core.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : IResult
{
    
}