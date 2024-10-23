using AutoMapper;

using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Communication.Request;
using CleanArchMvc.Communication.Response;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class ProductService(IProductRepository repository, IMapper mapper) : IProductService
{
    private readonly IProductRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task Add(ProductRequest request)
    {
        var product = _mapper.Map<Product>(request);

        await _repository.CreateAsync(product);
    }

    public async Task Delete(int? id)
    {
        var product = await _repository.GetByIdAsync(id);

        await _repository.RemoveAsync(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetAll()
    {
        var products = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse> GetById(int? id)
    {
        var product = await _repository.GetByIdAsync(id);

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> GetProductCategory(int? id)
    {
        var product = await _repository.GetProductCategoryAsync(id);

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task Update(ProductRequest request)
    {
        var product = _mapper.Map<Product>(request);

        await _repository.UpdateAsync(product);
    }
}
