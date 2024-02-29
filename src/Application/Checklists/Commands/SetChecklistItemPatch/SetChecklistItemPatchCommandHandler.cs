// using Application.Checklists.Dtos;
// using Application.Common.Exceptions;
// using Application.Common.Interfaces;
// using Domain.Entities.ChecklistAggregate;
// using MediatR;
// using Microsoft.AspNetCore.JsonPatch;
// using Microsoft.AspNetCore.Mvc;

// namespace Application.Checklists.Commands.SetChecklistCheckedValue;

// public class SetChecklistItemPatchCommandHandler : IRequestHandler<SetChecklistItemPatchCommand>
// {

//     private readonly IChecklistItemRepository _checklistItemRepository;


//     public SetChecklistItemPatchCommandHandler(
//         IChecklistItemRepository checklistItemRepository,
//         IChecklistItemQuestionRepository checklistItemQuestionRepository

//         )
//     {
//         _checklistItemRepository = checklistItemRepository;

//     }
//     public async Task Handle(SetChecklistItemPatchCommand request, CancellationToken cancellationToken)
//     {
//         var checklistItem = await _checklistItemRepository.GetChecklistItem(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(ChecklistItemQuestion), request.Id);
//         ChangeChecklistItem(checklistItem, request.Patches);
//         request.Patches.ApplyTo(checklistItem);
//         await _checklistItemRepository.SaveChanges(cancellationToken);
//     }

//     public void ChangeChecklistItem(ChecklistItem q, JsonPatchDocument<ChecklistItem> patches)
//     {
//         patches.ApplyTo(q);
//     }
// }