using Application.Checklists.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Checklists.Commands;

public class SetChecklistCheckedValueCommand : IRequest
{
    public Guid Id {get; set;} 

    public required JsonPatchDocument<ChecklistItemQuestionDto> Patch {get; set;}
}