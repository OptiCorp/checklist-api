

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
}