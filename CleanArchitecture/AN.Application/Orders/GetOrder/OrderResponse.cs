namespace AN.Application.Orders.GetOrder;

public record OrderResponse(Guid Id, Guid CustomerId, List<LineItemResponse> LineItems);