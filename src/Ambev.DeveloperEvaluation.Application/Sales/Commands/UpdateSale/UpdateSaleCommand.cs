using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleCommand : IRequest<SaleDto>
{
    public Guid Id { get; set; }
    public List<UpdateSaleItemDto> Items { get; set; } = new();
}
