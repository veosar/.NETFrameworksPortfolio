using Rebus.Sagas;

namespace AN.Application.Orders.Create;

public class OrderCreateSagaData : ISagaData
{
    public Guid Id { get; set; }
    public int Revision { get; set; }
    public Guid OrderId { get; set; }
    public bool ConfirmationEmailSent { get; set; }
    public bool PaymentRequestSent { get; set; }
}