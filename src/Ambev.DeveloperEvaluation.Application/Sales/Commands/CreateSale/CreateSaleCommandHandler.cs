using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleDto>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSaleCommandHandler(ISaleRepository repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<SaleDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var customer = new ExternalIdentity(request.CustomerId, request.CustomerName);
        var branch = new ExternalIdentity(request.BranchId, request.BranchName);
        var sale = new Sale(request.SaleNumber, request.SaleDate, customer, branch);

        foreach (var item in request.Items)
        {
            var product = new ExternalIdentity(item.ProductId, item.ProductName);
            sale.AddItem(product, item.Quantity, item.UnitPrice);
        }

        await _repository.AddAsync(sale, cancellationToken);
        _repository.SaveChanges();

        foreach (var e in sale.Events) await _mediator.Publish(e, cancellationToken);
        sale.ClearEvents();

        return _mapper.Map<SaleDto>(sale);
    }
}