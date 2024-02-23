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
            ?? throw new NotFoundException(nameof(Mobilization), request.Id);

        //TODO: needs to be handled differently if Items/Parts are to be moved to a separate table
        if(!mobilization.Checklist.Parts.Any(id => request.PartId == id )) throw new NotFoundException("Part", request.PartId);

        var checklistItem = await _checklistItemRepository.GetChecklistItemByItemId(request.PartId, mobilization.ChecklistId, cancellationToken) 
            ?? throw new NotFoundException(nameof(ChecklistItem), request.PartId);
       
        RemovePartFromMobilization(mobilization.Checklist, request.PartId);
    
        RemovePartFromChecklistItems(checklistItem);

        await _checklistRepository.SaveChanges(cancellationToken);
    }

    private void RemovePartFromMobilization(Checklist checklist, string Id)
    {
        _checklistRepository.RemovePartFromChecklist(checklist, Id);
    }

    private void RemovePartFromChecklistItems(ChecklistItem checklistItem)
    {
        //await _checklistItemRepository.DeleteChecklistItem(Id);
        _checklistItemRepository.RemoveChecklistItem(checklistItem);
    }
}
