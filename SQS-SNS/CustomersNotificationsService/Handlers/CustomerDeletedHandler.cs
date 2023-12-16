using CustomersContracts.Messages;
using CustomersNotificationsService.Models.Options;
using CustomersNotificationsService.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace CustomersNotificationsService.Handlers;

public class CustomerDeletedHandler : IRequestHandler<CustomerDeleted>
{
    private readonly ILogger<CustomerDeletedHandler> _logger;
    private readonly ICustomerEmailService _customerEmailService;
    
    public CustomerDeletedHandler(ILogger<CustomerDeletedHandler> logger, ICustomerEmailService customerEmailService)
    {
        _logger = logger;
        _customerEmailService = customerEmailService;
    }
    
    public async Task Handle(CustomerDeleted request, CancellationToken cancellationToken)
    {
        var comeBackDiscountCode = "TestDiscountCodeCustomerDeleted";
        await _customerEmailService.SendComeBackMessage(request.FirstName, request.LastName, request.Email,
            comeBackDiscountCode);
    }
}