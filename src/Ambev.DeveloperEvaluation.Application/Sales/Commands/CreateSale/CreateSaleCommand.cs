using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public class CreateSaleCommand : IRequest<SaleDto>
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; }
    public List<CreateSaleItemDto> Items { get; set; }
}

public class CreateSaleItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}