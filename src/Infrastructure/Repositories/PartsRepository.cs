using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class PartsRepository : IPartsRepository
{

    private readonly ModelContextBase _modelContextBase;

    public PartsRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }

    public async Task AddPart(Part part, CancellationToken cancellationToken)
    {
        await _modelContextBase.Parts.AddAsync(part, cancellationToken);

        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Part?> GetById(string id, CancellationToken cancellationToken)
    {
        var part =  await _modelContextBase.Parts
            .AsNoTracking()
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .Include(p => p.Children)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (part == null) return null;

        part.hasChecklistTemplate = part.PartTemplate.PartCheckListTemplate is not null;
        return part;
    }

    public async Task<IEnumerable<Part>> GetAll(bool includeChildren,CancellationToken cancellationToken)
    {
        var query = _modelContextBase.Parts.AsNoTracking();

        if (includeChildren){
            query = query.Include(p => p.Children);
        }

        query = query
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .Include(p => p.Children);

        var parts = await query.ToListAsync(cancellationToken);

        foreach (var part in parts)
        {
            part.hasChecklistTemplate = part.PartTemplate.PartCheckListTemplate is not null;
        }

        return parts;
    }
}