using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSalesList;

public class GetSalesListQueryHandler : IRequestHandler<GetSalesListQuery, List<SaleDto>>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    public GetSalesListQueryHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<SaleDto>> Handle(GetSalesListQuery request, CancellationToken cancellationToken)
    {
        var sales = await _repository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        return _mapper.Map<List<SaleDto>>(sales);
    }
}