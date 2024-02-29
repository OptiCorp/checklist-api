

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

    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistRepository _checklistRepository;
    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    private readonly IPunchRepository _punchRepository;

    public AddPunchCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistRepository checklistRepository,
        IChecklistQuestionRepository checklistQuestionRepository,
        IPunchRepository punchRepository
        )
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistRepository = checklistRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
        _punchRepository = punchRepository;
    }
    public async Task<Guid> Handle(AddPunchCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);;

        var checklist = await _checklistRepository.GetChecklistByItemId(request.ItemId, mobilization.ChecklistCollectionId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Checklist), request.ItemId);
        
        var newPunch = MapToPunch(request, checklist.Id);

        await _punchRepository.AddPunch(newPunch, cancellationToken);
        
        return newPunch.Id;
    }

    private Punch MapToPunch(AddPunchCommand request, Guid checklistId)
    {
        return Punch.New(request.Title, checklistId, request.Description);
    }
}