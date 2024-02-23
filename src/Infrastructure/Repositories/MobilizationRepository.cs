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
}
