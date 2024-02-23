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

    //    public async Task AssociatePunchWithUrl(Guid id, Uri blobUri, CancellationToken cancellationToken)
    //    {
    //        var punch = await _modelContextBase.Punches.SingleAsync(p => p.Id == id, cancellationToken);
    //        punch.ImageBlobUri = blobUri;
    //        await _modelContextBase.SaveChangesAsync(cancellationToken);
    //    }

    public async Task<Guid> AddPunch(Punch punch, CancellationToken cancellationToken)
    {
        await Add(punch, cancellationToken);
        return punch.Id;
    }

    //    public async Task<string> CreatePunch(string Title, string? Description, string checklistSectionId, CancellationToken cancellationToken)
    //    {
    //        var checklistSection = await _modelContextBase.ChecklistSections.SingleAsync(cs => cs.Id == checklistSectionId);
    //        var newPunch = new Punch
    //        {
    //            Title = Title,
    //            Description = Description,
    //            Section = checklistSection
    //        };
    //        await _modelContextBase.Punches.AddAsync(newPunch, cancellationToken);
    //        await _modelContextBase.SaveChangesAsync(cancellationToken);
    //        return newPunch.Id;
    //    }

    public async Task<Punch?> GetPunch(Guid id, CancellationToken cancellationToken)
    {
        var punch = await GetSet()
            .Include(p => p.ChecklistItem)
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

    //    public async Task<bool> PunchExists(string punchId, CancellationToken cancellationToken)
    //    {
    //        return await _modelContextBase.Punches
    //            .AsNoTracking()
    //            .AnyAsync(p => p.Id == punchId, cancellationToken);
    //    }
}
