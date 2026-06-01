using MediatR;

public class CancelSaleCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
