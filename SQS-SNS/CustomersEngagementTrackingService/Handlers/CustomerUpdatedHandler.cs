using CustomersContracts.Messages;
using CustomersEngagementTrackingService.Models.Options;
using CustomersEngagementTrackingService.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace CustomersEngagementTrackingService.Handlers;

public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdated>
{
    private readonly ILogger<CustomerUpdatedHandler> _logger;
    private readonly IOptions<EngagementOptions> _engagementOptions;
    private readonly ICustomerEmailService _customerEmailService;
    
    public CustomerUpdatedHandler(ILogger<CustomerUpdatedHandler> logger, IOptions<EngagementOptions> engagementOptions, ICustomerEmailService customerEmailService)
    {
        _logger = logger;
        _engagementOptions = engagementOptions;
        _customerEmailService = customerEmailService;
    }

    public async Task Handle(CustomerUpdated request, CancellationToken cancellationToken)
    {
        // This kind of logic validation could be done on SNS topic level, I'm only doing it here so it's visible in repository however I'm aware this could, and should
        // be moved into SNS
        if (request.OldTotalMoneySpent == request.NewTotalMoneySpent || 
            request.NewTotalMoneySpent < _engagementOptions.Value.TotalMoneySpentThreshold ||
            request.OldTotalMoneySpent > _engagementOptions.Value.TotalMoneySpentThreshold)
        {
            _logger.LogInformation("This message will not be processed as customers TotalMoneySpent has not changed or new value was below the threshold of {totalMoneySpentThreshold}", _engagementOptions.Value.TotalMoneySpentThreshold);
            return;
        }

        var discountCode = "TestDiscountCode";
        await _customerEmailService.SendDiscountCode(request.FirstName, request.LastName, request.Email, discountCode);
    }
}