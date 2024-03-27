
using Domain.Entities;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistTemplateRepository
{
    Task<ChecklistTemplate?> GetChecklistTemplateById(Guid id, CancellationToken cancellationToken = default);

    Task<ChecklistTemplate?> GetChecklistTemplateByItemTemplateId(string itemTemplateId, CancellationToken cancellationToken = default);

    Task<ChecklistTemplate> CreateChecklistTemplate(string itemTemplateId, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken);

}

