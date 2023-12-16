using CustomersContracts.Messages;
using CustomersNotificationsService.Models.Options;
using CustomersNotificationsService.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace CustomersNotificationsService.Handlers;

public class CustomerCreatedHandler : IRequestHandler<CustomerCreated>
{
    private readonly ILogger<CustomerCreatedHandler> _logger;
    private readonly ICustomerEmailService _customerEmailService;
    
    public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger, ICustomerEmailService customerEmailService)
    {
        _logger = logger;
        _customerEmailService = customerEmailService;
    }

    public async Task Handle(CustomerCreated request, CancellationToken cancellationToken)
    {
        var firstPurchaseDiscountCode = "TestDiscountCodeCustomerCreated";
        await _customerEmailService.SendFirstTimeDiscountCode(request.FirstName, request.LastName, request.Email,
            firstPurchaseDiscountCode);
    }
}