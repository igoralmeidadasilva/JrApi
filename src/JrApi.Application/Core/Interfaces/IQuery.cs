namespace JrApi.Application.Core.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : IQueryResponse;