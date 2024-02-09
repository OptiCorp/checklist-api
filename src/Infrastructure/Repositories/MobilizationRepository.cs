using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Infrastructure.Repositories;

public class MobilizationRepository : IMobilizationRepository
{
    private readonly ModelContextBase _modelContextBase;

    public MobilizationRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }

    public async Task AddMobilization(Mobilization mobilization, CancellationToken cancellationToken)
    {
        await _modelContextBase.Mobilizations.AddAsync(mobilization, cancellationToken);
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Mobilization?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _modelContextBase.Mobilizations
            .Include(x => x.Parts)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Mobilization>> GetAll(CancellationToken cancellationToken)
    {
        return await _modelContextBase.Mobilizations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteMobilization(string id, CancellationToken cancellationToken)
    {
        await _modelContextBase.Mobilizations
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .Include(m => m.Checklist)
                .ThenInclude(c => c.Parts)
            // .Include(m => m.Checklist)
            //     .ThenInclude(c => c.ChecklistSections
            //         .Where(cs => cs.PartId == partId
            //     ))
            .SingleAsync(m => m.Id == id, cancellationToken);


        var part = mob.Checklist.Parts.Single(p => p.Id == partId);
        mob.Checklist.Parts.Remove(part);

        var cs = await _modelContextBase.ChecklistSections
            .Where(cs => cs.ChecklistId == mob.ChecklistId && cs.PartId == partId)
            .SingleAsync(cancellationToken);

        _modelContextBase.ChecklistSections.Remove(cs);

        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken)
    {
        var query = _modelContextBase.Mobilizations
            .AsNoTracking()
            .Where(m => m.Id == mobId);

        if (includeChildren)
        {
            query = query
                .Include(m => m.Checklist)
                    .ThenInclude(c => c.Parts)
                        .ThenInclude(p => p.Children);
        }
        else
        {
            query = query
                .Include(m => m.Checklist)
                    .ThenInclude(c => c.Parts);
        }
        var checklist = await query
            .Select(m => m.Checklist)
            .SingleAsync(cancellationToken);

        return checklist.Parts;
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }
}
