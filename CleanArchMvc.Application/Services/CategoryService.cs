using AutoMapper;

using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Communication.Request;
using CleanArchMvc.Communication.Response;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService(ICategoryRepository repository, IMapper mapper) : ICategoryService
{
    private readonly ICategoryRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task Add(CategoryRequest request)
    {
        var category = _mapper.Map<Category>(request);

        await _repository.CreateAsync(category);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAll()
    {
        var categories = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
    }

    public async Task<CategoryResponse> GetById(int? id)
    {
        var category = await _repository.GetByIdAsync(id);

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task Remove(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        await _repository.RemoveAsync(category);
    }

    public async Task Update(CategoryRequest request)
    {
        var category = _mapper.Map<Category>(request);

        await _repository.UpdateAsync(category);
    }
}
