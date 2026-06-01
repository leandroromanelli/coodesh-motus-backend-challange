using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSalesList;

public class GetSalesListQuery : IRequest<List<SaleDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}