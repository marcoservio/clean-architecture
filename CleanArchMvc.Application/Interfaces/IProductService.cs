using CleanArchMvc.Communication.Request;
using CleanArchMvc.Communication.Response;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAll();
    Task<ProductResponse> GetById(int? id);
    Task<ProductResponse> GetProductCategory(int? id);
    Task Add(ProductRequest request);
    Task Update(ProductRequest request);
    Task Delete(int? id);
}
