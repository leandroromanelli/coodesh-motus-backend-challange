using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, SaleDto>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateSaleCommandHandler(ISaleRepository repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<SaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new Exception("Sale not found.");
        if (sale.IsCancelled)
            throw new Exception("Cannot update a cancelled sale.");

        // Processar cada item do request
        foreach (var itemDto in request.Items)
        {
            if (itemDto.IsDeleted && itemDto.Id.HasValue)
            {
                // Remover item existente
                sale.CancelItem(itemDto.Id.Value);
            }
            else if (itemDto.Id.HasValue)
            {
                // Atualizar quantidade de item existente
                sale.UpdateItemQuantity(itemDto.Id.Value, itemDto.Quantity);
            }
            else
            {
                // Adicionar novo item
                var product = new ExternalIdentity(itemDto.ProductId, itemDto.ProductName);
                sale.AddItem(product, itemDto.Quantity, itemDto.UnitPrice);
            }
        }

        _repository.Update(sale); // Salva alterações (dentro do repositório já chama SaveChanges)
        // Publicar eventos de domínio
        foreach (var e in sale.Events)
            await _mediator.Publish(e, cancellationToken);
        sale.ClearEvents();

        return _mapper.Map<SaleDto>(sale);
    }
}