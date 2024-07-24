using System.Transactions;
using AN.Application.Data;
using AN.Application.Messaging;
using MediatR;

namespace AN.Application.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!request.GetType().Name.EndsWith("Command"))
        {
            return await next();
        }
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        transactionScope.Complete();
        return result;
    }
}