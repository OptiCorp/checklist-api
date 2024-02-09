using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Common.Interfaces;

public interface IMobilizationRepository
{
    Task AddMobilization(Mobilization mobilization, CancellationToken cancellationToken = default);

    Task<Mobilization?> GetById(string id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Mobilization>> GetAll(CancellationToken cancellationToken = default);

    Task DeleteMobilization(string id, CancellationToken cancellationToken = default);

    //Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
