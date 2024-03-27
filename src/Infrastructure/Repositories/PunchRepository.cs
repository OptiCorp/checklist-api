using Microsoft.EntityFrameworkCore;
using Application.Punches;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using Application.Punches.Dtos;
using Infrastructure.Repositories.Common;
using Domain.Entities;

namespace MobDeMob.Infrastructure.Repositories;

public class PunchRepository : RepositoryBase<Punch>, IPunchRepository
{


    public PunchRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {

    }
    public async Task<Guid> AddPunch(Punch punch, CancellationToken cancellationToken)
    {
        await Add(punch, cancellationToken);
        return punch.Id;
    }

    public async Task<Punch?> GetPunchNoTracking(Guid id, CancellationToken cancellationToken)
    {
        var punch = await GetSet()
            .AsNoTracking()
            .Include(p => p.Checklist)
            .Include(p => p.PunchFiles)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return punch;
    }

    public async Task<int> GetPunchesCount(Guid checklistId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Where(p => p.ChecklistId == checklistId)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Punch>> GetPunchesForChecklist(Guid checklistId, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.Punches
            .Where(p => p.ChecklistId == checklistId)
            .Include(p => p.PunchFiles)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Guid>> GetPunchIds(Guid checklistId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Where(p => p.ChecklistId == checklistId)
            .Select(p => p.Id).ToListAsync(cancellationToken);
    }

    public async Task<Punch?> GetPunch(Guid id, CancellationToken cancellationToken = default)
    {
        var punch = await GetSet()
            .Include(p => p.PunchFiles)
            .Include(p => p.Checklist)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return punch;
    }
}
