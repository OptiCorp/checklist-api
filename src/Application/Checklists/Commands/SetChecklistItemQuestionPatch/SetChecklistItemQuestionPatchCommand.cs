using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Checklists.Commands;
public class SetChecklistItemQuestionPatchCommand : IRequest
{
    public Guid Id {get; set;} 

    public required JsonPatchDocument<ChecklistItemQuestion> Patches {get; set;}
}