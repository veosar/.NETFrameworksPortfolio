using MediatR;

namespace AN.Application.Messaging;

public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase;
public interface ICommand : IRequest, ICommandBase;
