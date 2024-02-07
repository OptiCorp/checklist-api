using System.Data.Common;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Common.Interfaces;

public interface IMobilizationRepository
{
    Task AddMobilization(Mobilization mobilization, CancellationToken cancellationToken);

    Task<Mobilization?> GetById(string id, CancellationToken cancellationToken);

    Task<IEnumerable<Mobilization>> GetAll(CancellationToken cancellationToken);

    Task UpdateMobilization(string id ,string? title, string? desription ,CancellationToken cancellationToken);

    Task DeleteMobilization(string id, CancellationToken cancellationToken);

    Task AddPartToMobilization(string id, string partId, CancellationToken cancellationToken);

    Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken);

    Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken);

    
}
