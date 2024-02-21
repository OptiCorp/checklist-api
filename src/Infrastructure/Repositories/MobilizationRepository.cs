using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Infrastructure.Repositories;

public class MobilizationRepository : RepositoryBase<Mobilization>, IMobilizationRepository
{
    public MobilizationRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<Guid> AddMobilization(Mobilization mobilization, CancellationToken cancellationToken)
    {
        await Add(mobilization, cancellationToken);
        return mobilization.Id;
    }

    public async Task<Mobilization?> GetMobilizationById(Guid mobilizationId, CancellationToken cancellationToken)
        => await GetSet().Include(m => m.Checklist).FirstOrDefaultAsync(x => x.Id == mobilizationId, cancellationToken);

    // public async Task<IEnumerable<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken)
    //     => await GetAll(cancellationToken);

    public async Task<IEnumerable<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken)
    {
        return await GetSet()
            .AsNoTracking()
            .Include(m => m.Checklist)
            .ThenInclude(c => c.ChecklistItems)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteMobilization(Guid id, CancellationToken cancellationToken)
        => await DeleteById(id, cancellationToken);

    //TODO: this was created for getting completion percent, may be a better way of doing it without including everything
    public async Task<Mobilization?> GetMobilizationByIdWithChecklistItems(Guid mobilizationId, CancellationToken cancellationToken = default)

        => await GetSet().Include(m => m.Checklist)
                            .ThenInclude(c => c.ChecklistItems)
                                .ThenInclude(ci => ci.Questions)
                                .SingleOrDefaultAsync(x => x.Id == mobilizationId, cancellationToken);

    //public async Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken)
    //{
    //    var mob = await _modelContextBase.Mobilizations
    //        .Include(m => m.Checklist)
    //            .ThenInclude(c => c.Parts)
    //        // .Include(m => m.Checklist)
    //        //     .ThenInclude(c => c.ChecklistSections
    //        //         .Where(cs => cs.PartId == partId
    //        //     ))
    //        .SingleAsync(m => m.Id == id, cancellationToken);


    //    var part = mob.Checklist.Parts.Single(p => p.Id == partId);
    //    mob.Checklist.Parts.Remove(part);

    //    var cs = await _modelContextBase.ChecklistSections
    //        .Where(cs => cs.ChecklistId == mob.ChecklistId && cs.PartId == partId)
    //        .SingleAsync(cancellationToken);

    //    _modelContextBase.ChecklistSections.Remove(cs);

    //    await _modelContextBase.SaveChangesAsync(cancellationToken);
    //}

    //public async Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken)
    //{
    //    var query = _modelContextBase.Mobilizations
    //        .AsNoTracking()
    //        .Where(m => m.Id == mobId);

    //    if (includeChildren)
    //    {
    //        query = query
    //            .Include(m => m.Checklist)
    //                .ThenInclude(c => c.Parts)
    //                    .ThenInclude(p => p.Children);
    //    }
    //    else
    //    {
    //        query = query
    //            .Include(m => m.Checklist)
    //                .ThenInclude(c => c.Parts);
    //    }
    //    var checklist = await query
    //        .Select(m => m.Checklist)
    //        .SingleAsync(cancellationToken);

    //    return checklist.Parts;
    //}
}
