namespace JrApi.Application.Core.Interfaces;

public interface ICommand<TResponse> : IRequest<TResponse> where TResponse : ICommandResponse;