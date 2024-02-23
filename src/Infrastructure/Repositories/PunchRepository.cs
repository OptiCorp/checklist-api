using Microsoft.EntityFrameworkCore;
using Application.Punches;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using Application.Punches.Dtos;
using Infrastructure.Repositories.Common;

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
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return punch;
    }

    public async Task<int> GetPunchesCount(Guid checklistItemId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Where(p => p.ChecklistItemId == checklistItemId)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Punch>> GetPunchesForChecklistItem(Guid checklistItemId, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.Punches
            .Where(p => p.ChecklistItemId == checklistItemId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Guid>> GetPunchIds(Guid checklistItemId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Where(p => p.ChecklistItemId == checklistItemId)
            .Select(p => p.Id).ToListAsync(cancellationToken);
    }

    public async Task<Punch?> GetPunch(Guid id, CancellationToken cancellationToken = default)
    {
        var punch = await GetSet()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return punch;
    }
}
