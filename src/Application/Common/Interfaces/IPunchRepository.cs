using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Punches;
public interface IPunchRepository
{
    //Task<string> CreatePunch(string Title, string? Description, string checklistSectionId, CancellationToken cancellationToken);

    Task<Punch?> GetPunch(Guid id, CancellationToken cancellationToken);

    //    Task<bool> PunchExists(string punchId, CancellationToken cancellationToken);
    Task<Guid> CreatePunch(Punch newPunch, CancellationToken cancellationToken);

    Task SaveChanges (CancellationToken cancellationToken = default);
}
