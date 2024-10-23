using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(ApplicationDbContext context) : RepositoryBase<Category>(context), ICategoryRepository
{

}
