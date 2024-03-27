

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches;
using Domain.Entities;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Checklists.Commands.AddPunch;

public class AddPunchCommandHandler : IRequestHandler<AddPunchCommand, Guid>
{
    private readonly IChecklistRepository _checklistRepository;
    private readonly IPunchRepository _punchRepository;

    public AddPunchCommandHandler(
        IChecklistRepository checklistRepository,
        IPunchRepository punchRepository
        )
    {
        _checklistRepository = checklistRepository;
        _punchRepository = punchRepository;
    }
    public async Task<Guid> Handle(AddPunchCommand request, CancellationToken cancellationToken)
    {

        var checklist = await _checklistRepository.GetSingleChecklist(request.checklistId, cancellationToken)
            ?? throw new NotFoundException(nameof(Checklist), request.checklistId);

        var newPunch = MapToPunch(request, checklist.Id);

        await _punchRepository.AddPunch(newPunch, cancellationToken);

        return newPunch.Id;
    }

    private Punch MapToPunch(AddPunchCommand request, Guid checklistId)
    {
        return Punch.New(request.title, checklistId, request.description);
    }
}