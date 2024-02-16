

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Checklists.Commands.AddPunch;

public class AddPunchCommandHandler : IRequestHandler<AddPunchCommand, Guid>
{

    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistItemRepository _checklistItemRepository;
    private readonly IChecklistItemQuestionRepository _checklistItemQuestionRepository;

    private readonly IPunchRepository _punchRepository;

    public AddPunchCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistItemRepository checklistItemRepository,
        IChecklistItemQuestionRepository checklistItemQuestionRepository,
        IPunchRepository punchRepository
        )
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistItemRepository = checklistItemRepository;
        _checklistItemQuestionRepository = checklistItemQuestionRepository;
        _punchRepository = punchRepository;
    }
    public async Task<Guid> Handle(AddPunchCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);;

        var checklistItem = await _checklistItemRepository.GetChecklistItemByItemId(request.ItemId, mobilization.ChecklistId, cancellationToken) ?? throw new Exception("ChecklistItem not found");
        
        var newPunch = MapToPunch(request, checklistItem.Id);

        await _punchRepository.AddPunch(newPunch, cancellationToken);
        
        return newPunch.Id;
    }

    private Punch MapToPunch(AddPunchCommand request, Guid checklistItemId)
    {
        return new Punch{
            Title = request.Title,
            Description = request.Description,
            ChecklistItemId = checklistItemId
        };
    }
}