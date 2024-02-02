using System.Data.Common;
using MobDeMob.Domain.Entities.Mobilization;

namespace Application.Common.Interfaces;

public interface IMobilizationRepository
{
    Task AddMobilization(Mobilization mobilization);

    Task<Mobilization?> GetById(string id, CancellationToken cancellationToken);

    Task<IEnumerable<Mobilization>> GetAll(CancellationToken cancellationToken);

    Task UpdateMobilization(string id ,string? title, string? desription ,CancellationToken cancellationToken);

    Task DeleteMobilization(string id, CancellationToken cancellationToken);
}
