using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Checklists.Commands;

public class SetChecklistItemPatchCommand : IRequest
{
    public Guid Id {get; set;} 

    public required JsonPatchDocument<ChecklistItem> Patches {get; set;}
}