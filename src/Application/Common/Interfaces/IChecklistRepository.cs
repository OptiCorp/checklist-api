

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistRepository
{
    Task<string> AddChecklist();
}