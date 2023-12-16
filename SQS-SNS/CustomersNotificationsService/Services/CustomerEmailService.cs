using CustomersNotificationsService.Models.Options;
using FluentEmail.Core;
using Microsoft.Extensions.Options;

namespace CustomersNotificationsService.Services;

public interface ICustomerEmailService
{
    public Task SendFirstTimeDiscountCode(string firstName, string lastName, string email, string discountCode);
    public Task SendComeBackMessage(string firstName, string lastName, string email, string discountCode);
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

    public async Task SendFirstTimeDiscountCode(string firstName, string lastName, string email, string discountCode)
    {
        var template =
            "Dear @Model.FirstName @Model.LastName,\nThank you for registration. Specially for you, we have generated this one-time, 10% discount code that you can use in your first purchase: @Model.DiscountCode!";

        var emailObject = _fluentEmail
            .To(email)
            .Subject("You are getting a discount code!")
            .UsingTemplate(template,
                new { FirstName = firstName, LastName = lastName, DiscountCode = discountCode });

        await emailObject.SendAsync();
    }

    public async Task SendComeBackMessage(string firstName, string lastName, string email, string discountCode)
    {
        var template =
            "Dear @Model.FirstName @Model.LastName,\nWe are very sorry that you've decided to leave us. Specially for you, we have generated this one-time, 10% discount code that you can use in your next purchase, if you decide to come back to us: @Model.DiscountCode";

        var emailObject = _fluentEmail
            .To(email)
            .Subject("Discount code")
            .UsingTemplate(template,
                new { FirstName = firstName, LastName = lastName, DiscountCode = discountCode });

        await emailObject.SendAsync();
    }
}