using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Punches;
public interface IPunchRepository
{
    //Task<string> CreatePunch(string Title, string? Description, string checklistSectionId, CancellationToken cancellationToken);

    Task<Punch?> GetPunchNoTracking(Guid id, CancellationToken cancellationToken = default);

    Task<Punch?> GetPunch(Guid id, CancellationToken cancellationToken = default);


    //    Task<bool> PunchExists(string punchId, CancellationToken cancellationToken);
    Task<Guid> AddPunch(Punch newPunch, CancellationToken cancellationToken = default);

    Task<IEnumerable<Punch>> GetPunchesForChecklistItem(Guid checklistItemId, CancellationToken cancellationToken = default);

    Task<int> GetPunchesCount (Guid checklistItemId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Guid>> GetPunchIds (Guid checklistItemId, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);

    
}
