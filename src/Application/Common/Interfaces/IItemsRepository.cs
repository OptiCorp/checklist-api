using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Common.Interfaces;
public interface IPartsRepository
{
    Task AddPart(Part part, CancellationToken cancellationToken);
    Task<Part?> GetById(string id, CancellationToken cancellationToken);

    Task<IEnumerable<Part>> GetAll (CancellationToken cancellationToken);

    Task<IEnumerable<ChecklistSectionTemplate>> GetQuestions (string id,CancellationToken cancellationToken);   

}
