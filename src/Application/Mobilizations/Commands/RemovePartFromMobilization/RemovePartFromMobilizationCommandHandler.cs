using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommandHandler : IRequestHandler<RemovePartFromMobilizationCommand>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly IChecklistRepository _checklistRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;
    public RemovePartFromMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository, IChecklistItemRepository checklistItemRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository;
        _checklistItemRepository = checklistItemRepository;
        
    }

    public async Task Handle(RemovePartFromMobilizationCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.Id); // same as above

        if(!mobilization.Checklist.Parts.Any(id => request.PartId == id )) throw new NotFoundException("Part", request.PartId);

        var checklistItem = await _checklistItemRepository.GetChecklistItemByItemId(request.PartId, mobilization.ChecklistId, cancellationToken) ?? throw new NotFoundException(nameof(ChecklistItem), request.PartId);
       
        RemovePartFromMobilization(mobilization.Checklist, request.PartId);
    
        await RemovePartFromChecklistItems(checklistItem.Id);

        await _checklistRepository.SaveChanges(cancellationToken);
    }

    private void RemovePartFromMobilization(Checklist checklist, string Id)
    {
        _checklistRepository.RemovePartFromChecklist(checklist, Id);
    }

    private async Task RemovePartFromChecklistItems(Guid Id)
    {
        await _checklistItemRepository.DeleteChecklistItem(Id);
    }
}
