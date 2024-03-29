using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Common.Interfaces;
public interface IPartsRepository
{
    Task AddPart(Part part, CancellationToken cancellationToken);
    Task<Part?> GetById(string id, CancellationToken cancellationToken);

    Task<IEnumerable<Part>> GetAll(bool includeChildren, CancellationToken cancellationToken);

}
