using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Punches;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Infrastructure.Repositories;

public class PunchRepository : IPunchRepository
{
    private readonly ModelContextBase _modelContextBase;

    public PunchRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }
    public async Task AssociatePunchWithUrl(string id, Uri blobUri, CancellationToken cancellationToken)
    {
        var punch = await _modelContextBase.Punches.SingleAsync(p => p.Id == id, cancellationToken);
        punch.ImageBlobUri = blobUri;
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> CreatePunch(string Title, string? Description, string checklistSectionId, CancellationToken cancellationToken)
    {
        var checklistSection = await _modelContextBase.ChecklistSections.SingleAsync(cs => cs.Id == checklistSectionId);
        var newPunch = new Punch
        {
            Title = Title,
            Description = Description,
            Section = checklistSection
        };
        await _modelContextBase.Punches.AddAsync(newPunch, cancellationToken);
        await _modelContextBase.SaveChangesAsync(cancellationToken);
        return newPunch.Id;
    }

    public async Task<ICollection<Punch>> GetAllPartPunches(string partId, CancellationToken cancellationToken)
    {
        var punches = await _modelContextBase.ChecklistSections
            .AsNoTracking()
            .Include(p => p.Punches)
            .Where(cs => cs.PartId == partId)
            .SelectMany(cs => cs.Punches)
            .ToListAsync(cancellationToken);
            
        return punches;
    }

    public async Task<Punch?> GetPunch(string id, CancellationToken cancellationToken)
    {
        var punch =  await _modelContextBase.Punches
            .AsNoTracking()
            .Include(p => p.Section)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (punch == null) return null;

        punch.PartId = punch.Section.PartId;
        punch.ChecklistId = punch.Section.ChecklistId;
        punch.ParantChecklistSectionId = punch.Section.ChecklistSectionId ?? punch.Section.Id; //The top section which has checklistsection children
        return punch;
    }

    public async Task<bool> PunchExists(string punchId, CancellationToken cancellationToken)
    {
        return await _modelContextBase.Punches
            .AsNoTracking()
            .AnyAsync(p => p.Id == punchId, cancellationToken);
    }
}