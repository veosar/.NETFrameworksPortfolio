using AN.Domain.Exceptions.Entities.Shared;

namespace AN.Domain.Entities.Shared;

public record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string value)
    {
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(value);
            return new Email(mailAddress.Address);
        }
        catch (Exception ex)
        {
            throw new InvalidEmailDomainException(value, ex);
        }
    }
}