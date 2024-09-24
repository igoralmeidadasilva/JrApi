using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Core.Interfaces;
public interface ICommand<TResponse> : IRequest<TResponse> where TResponse : Result
{
    
}