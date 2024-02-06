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
        return await _modelContextBase.Parts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    }

    public async Task<IEnumerable<Part>> GetAll(CancellationToken cancellationToken)
    {
        return await _modelContextBase.Parts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}