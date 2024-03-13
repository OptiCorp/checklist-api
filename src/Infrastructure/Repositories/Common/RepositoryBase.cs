using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Common;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories.Common;

public abstract class RepositoryBase<T> where T : Entity
{
    protected readonly ModelContextBase _modelContextBase;




    public RepositoryBase(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;

    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
        => await _modelContextBase.SaveChangesAsync(cancellationToken);

    protected DbSet<T> GetSet() => _modelContextBase.Set<T>();

    protected async Task<T> Add(T entity, CancellationToken cancellationToken = default)
    {
        await GetSet().AddAsync(entity, cancellationToken);
        await SaveChanges(cancellationToken);

        return entity;
    }

    protected async Task<T?> GetById(Guid id, CancellationToken cancellationToken = default)
        => await GetSet().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    protected async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        => await GetSet().AsNoTracking().ToListAsync(cancellationToken);

    protected async Task DeleteById(Guid id, CancellationToken cancellationToken = default)
        => await GetSet().Where(m => m.Id == id).ExecuteDeleteAsync(cancellationToken);

    protected void Remove(T entity)
        => _modelContextBase.Remove(entity);
}
