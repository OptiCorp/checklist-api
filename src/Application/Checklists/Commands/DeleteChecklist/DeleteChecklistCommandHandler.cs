using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Checklists.Commands;

public class DeleteChecklistCommandHandler : IRequestHandler<DeleteChecklistCommand>
{

    private readonly IChecklistRepository _checklistRepository;
    public DeleteChecklistCommandHandler(IChecklistRepository checklistRepository)
    {
        _checklistRepository = checklistRepository;

    }

    public async Task Handle(DeleteChecklistCommand request, CancellationToken cancellationToken)
    {

        var checklist = await _checklistRepository.GetSingleChecklist(request.checklistId, cancellationToken)
            ?? throw new NotFoundException(nameof(Checklist), request.checklistId);

        await _checklistRepository.DeleteChecklistById(checklist.Id, cancellationToken);
    }
}
