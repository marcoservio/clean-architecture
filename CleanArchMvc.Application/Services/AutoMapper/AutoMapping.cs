using AutoMapper;

using CleanArchMvc.Communication.Request;
using CleanArchMvc.Communication.Response;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
        RequestToResponse();
    }

    public void RequestToDomain()
    {
        CreateMap<CategoryRequest, Category>().ReverseMap();
        CreateMap<ProductRequest, Product>().ReverseMap();
    }

    public void DomainToResponse()
    {
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
    }

    public void RequestToResponse()
    {
        CreateMap<CategoryRequest, CategoryResponse>().ReverseMap();
        CreateMap<ProductRequest, ProductResponse>().ReverseMap();
    }
}
