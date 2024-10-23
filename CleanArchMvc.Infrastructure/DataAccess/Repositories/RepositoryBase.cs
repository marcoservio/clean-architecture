using CleanArchMvc.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infrastructure.DataAccess.Repositories;

public abstract class RepositoryBase<T>(ApplicationDbContext context) : IRepositoryBase<T> where T : class
{
    protected readonly ApplicationDbContext _context = context;

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int? id)
    {
        return (await _context.Set<T>().FindAsync(id))!;
    }

    public async Task RemoveAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
