using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Punches;
public interface IPunchRepository
{
    Task<string> CreatePunch (string Title, string? Description, string checklistSectionId, CancellationToken cancellationToken);
    Task AssociatePunchWithUrl (string id, Uri blobUri, CancellationToken cancellationToken);

    Task<Punch?> GetPunch (string id, CancellationToken cancellationToken);

    Task<Boolean> PunchExists (string punchId, CancellationToken cancellationToken);
}