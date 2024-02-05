

using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities.Mobilization;

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
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Mobilization>> GetAll(CancellationToken cancellationToken)
    {
        return await _modelContextBase.Mobilizations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateMobilization(string id, string? title, string? desription, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .SingleAsync(m => m.Id == id, cancellationToken);
        if (title != null)
        {
            mob.Title = title;
        }
        if (desription != null)
        {
            mob.Description = desription;
        }
        await _modelContextBase.SaveChangesAsync(cancellationToken);
        //TODO: should the items also be provided or should a different endpoint do this?
    }

    public async Task DeleteMobilization(string id, CancellationToken cancellationToken)
    {
        await _modelContextBase.Mobilizations
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task AddPartToMobilization(string id, string partId, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .AsNoTracking()
            .Include(m => m.Checklist)
            .FirstAsync(m => m.Id == id, cancellationToken);
        
        if (mob.ChecklistId == null)
        {
            throw new Exception($"Mobilizaiton with id: {id} does not have any Checklist associated");
        }

        var part = await _modelContextBase.Parts
            .FirstAsync(p => p.Id == partId, cancellationToken);

        if (part.ChecklistId != null)
        {
            throw new Exception($"PartId: '{partId}' already belongs to a different mobilization");
        }

        part.ChecklistId = mob.ChecklistId;
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .Include(m => m.Checklist)
            .ThenInclude(c => c.Parts)
            .FirstAsync(m => m.Id == id, cancellationToken);

        foreach (var m in mob.Parts)
        {
            if (m.Id == partId)
            {
                mob.Checklist.Parts.Remove(m);
                break;
            }
        }
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }
}