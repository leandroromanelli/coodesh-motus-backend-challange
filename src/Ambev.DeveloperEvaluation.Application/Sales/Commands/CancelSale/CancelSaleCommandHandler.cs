using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
{
    private readonly ISaleRepository _repository;
    private readonly IMediator _mediator;
    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id);
        sale.CancelSale();
        _repository.Update(sale);
        _repository.SaveChanges();

        foreach (var e in sale.Events) await _mediator.Publish(e);
        return true;
    }
}