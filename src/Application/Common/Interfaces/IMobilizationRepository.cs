using MobDeMob.Domain.Entities;

namespace Application.Common.Interfaces;

public interface IMobilizationRepository
{
    Task<string> AddMobilization(Mobilization mobilization, CancellationToken cancellationToken = default);

    Task<Mobilization?> GetMobilizationById(string id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken = default);

    Task DeleteMobilization(string id, CancellationToken cancellationToken = default);

    //Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken = default);

    //Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
