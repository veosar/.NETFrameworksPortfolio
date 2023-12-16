using CustomersEngagementTrackingService.Models.Options;
using FluentEmail.Core;
using Microsoft.Extensions.Options;

namespace CustomersEngagementTrackingService.Services;

public interface ICustomerEmailService
{
    public Task SendDiscountCode(string firstName, string lastName, string email, string discountCode);
}

public class CustomerEmailService : ICustomerEmailService
{
    private readonly IOptions<EmailOptions> _emailOptions;
    private readonly IFluentEmail _fluentEmail;
    public CustomerEmailService(IOptions<EmailOptions> emailOptions, IFluentEmail fluentEmail)
    {
        _emailOptions = emailOptions;
        _fluentEmail = fluentEmail;
    }

    public async Task SendDiscountCode(string firstName, string lastName, string email, string discountCode)
    {
        var template =
            "Dear @Model.FirstName @Model.LastName,\nThank you for your constant interest in our products. Specially for you, we have generated this one-time, 10% discount code that you can use in your next purchase: @Model.DiscountCode!";
        
        var emailObject = _fluentEmail
            .To(email)
            .Subject("You are getting a discount code!")
            .UsingTemplate(template,
                new { FirstName = firstName, LastName = lastName, DiscountCode = discountCode });

        await emailObject.SendAsync();
    }
}