using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class PartsRepository : IPartsRepository
{
    private readonly ModelContextBase _modelContextBase;

    public PartsRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }

    public async Task AddPart(Part part, CancellationToken cancellationToken = default)
    {
        await _modelContextBase.Parts.AddAsync(part, cancellationToken);

        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Part?> GetById(string id, CancellationToken cancellationToken = default)
    {
        var part = await _modelContextBase.Parts
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .Include(p => p.Children)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return part;
    }

    public async Task<IEnumerable<Part>> GetAll(bool includeChildren, CancellationToken cancellationToken = default)
    {
        var query = _modelContextBase.Parts.AsNoTracking();

        if (includeChildren)
        {
            query = query.Include(p => p.Children);
        }

        query = query
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate);

        var parts = await query.ToListAsync(cancellationToken);

        return parts;
    }

    public Task Delete(Part part)
    {
        throw new NotImplementedException();//TODO
    }
}
