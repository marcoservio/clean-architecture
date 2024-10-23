using CleanArchMvc.Communication.Request;
using CleanArchMvc.Communication.Response;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAll();
    Task<CategoryResponse> GetById(int? id);
    Task Add(CategoryRequest request);
    Task Update(CategoryRequest request);
    Task Remove(int id);
}
