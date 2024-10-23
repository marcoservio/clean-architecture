using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infrastructure.DataAccess.Repositories;

public class ProductRepository(ApplicationDbContext context) : RepositoryBase<Product>(context), IProductRepository
{
    public async Task<Product> GetProductCategoryAsync(int? id)
    {
        var produtos = (await GetAllAsync()).AsQueryable();

        return (await produtos.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id))!;
    }
}
