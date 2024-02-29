using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommandHandler : IRequestHandler<RemoveItemFromMobilizationCommand>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly IChecklistCollectionRepository _checklistCollectionRepository;

    private readonly IChecklistRepository _checklistRepository;
    public RemovePartFromMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistCollectionRepository checklistCollectionRepository, IChecklistRepository checklistRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository;
        _checklistCollectionRepository = checklistCollectionRepository;
        
    }

    public async Task Handle(RemoveItemFromMobilizationCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.Id);

        var checklist = await _checklistRepository.GetChecklistByItemId(request.ItemId, mobilization.ChecklistCollectionId, cancellationToken) 
            ?? throw new NotFoundException(nameof(Checklist), request.ItemId);
       
    
        DeleteChecklist(checklist);

        await _checklistRepository.SaveChanges(cancellationToken);
    }


    private void DeleteChecklist(Checklist checklist)
    {
        //await _checklistItemRepository.DeleteChecklistItem(Id);
        _checklistRepository.DeleteChecklistById(checklist.Id);
    }
}
