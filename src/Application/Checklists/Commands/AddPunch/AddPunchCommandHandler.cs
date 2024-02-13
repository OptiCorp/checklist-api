

using Application.Common.Exceptions;
using Application.Common.Interfaces;
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

    public AddPunchCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistItemRepository checklistItemRepository,
        IChecklistItemQuestionRepository checklistItemQuestionRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistItemRepository = checklistItemRepository;
        _checklistItemQuestionRepository = checklistItemQuestionRepository;
    }
    public async Task<Guid> Handle(AddPunchCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);;

        var checklistItem = await _checklistItemRepository.GetChecklistItemByItemId(request.ItemId, mobilization.ChecklistId) ?? throw new Exception("ChecklistItem not found");
        
        var newPunch = MapToPunch(request, checklistItem.Id);

        checklistItem.Punches.Add(newPunch);
        await _checklistItemRepository.SaveChanges(cancellationToken);
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