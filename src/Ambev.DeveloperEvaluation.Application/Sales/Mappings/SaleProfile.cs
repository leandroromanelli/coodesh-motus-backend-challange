using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<Sale, SaleDto>()
            .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.Customer.Id))
            .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.Name))
            .ForMember(d => d.BranchId, opt => opt.MapFrom(s => s.Branch.Id))
            .ForMember(d => d.BranchName, opt => opt.MapFrom(s => s.Branch.Name));
        CreateMap<SaleItem, SaleItemDto>()
            .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product.Id))
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));
    }
}