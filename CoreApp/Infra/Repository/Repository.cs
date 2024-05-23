using System.Linq.Expressions;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace InfraDatabase.Repository;

/// <inheritdoc />
public class Repository<T> : IRepository<T> where T : class
{
    private readonly CalangoContext _calangoContext;
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="calangoContext"></param>
    public Repository(CalangoContext calangoContext)
    {
        _calangoContext = calangoContext;
    }

    /// <inheritdoc />
    public async Task<T?> FirstAsync(Expression<Func<T, bool>> predicate)
    {
        var dado = await _calangoContext.Set<T>().FirstOrDefaultAsync(predicate);
        return dado;
    }

    /// <inheritdoc />
    public async Task<T?> FirstAsync(Expression<Func<T, bool>> predicate, bool asNoTraking)
    {
        if (asNoTraking)
        {
            return await _calangoContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        return await _calangoContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    /// <inheritdoc />
    public async Task<T?> FirstAsync(bool asNoTraking)
    {
        if (asNoTraking)
        {
            return await _calangoContext.Set<T>().AsNoTracking().FirstOrDefaultAsync();
        }

        return await _calangoContext.Set<T>().FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<T?> FirstAsync()
    {
        return await _calangoContext.Set<T>().FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _calangoContext.Set<T>().Where(predicate).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<bool> AnyAsync()
    {
        return await _calangoContext.Set<T>().AnyAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var data = _calangoContext.Set<T>().Where(predicate);
        return await data.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> AllAsync(string[] includes)
    {
        var db = _calangoContext.Set<T>();

        foreach (var i in includes)
        {
            db.Include(i);
        }
        return await db.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> AllAsync()
    {
        return await _calangoContext.Set<T>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> AllAsync(bool asNoTracking)
    {
        if (asNoTracking)
            return await _calangoContext.Set<T>().AsNoTracking().ToListAsync();
        return await _calangoContext.Set<T>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(T entity)
    {
        await _calangoContext.Set<T>().AddAsync(entity);
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        _calangoContext.Set<T>().Remove(entity);
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        _calangoContext.Entry(entity).State = EntityState.Modified;
        _calangoContext.Set<T>().Update(entity);
    }
}