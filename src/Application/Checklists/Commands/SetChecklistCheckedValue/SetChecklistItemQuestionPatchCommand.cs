using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Checklists.Commands;

//TODO: maybe not optimal, I think it fails if Patches is written as "Patches"
//TODO: maybe verify more than just ChecklistItemQuestion
public class SetChecklistItemQuestionPatchCommand : IRequest
{
    public Guid Id {get; set;} 

    public required JsonPatchDocument<ChecklistItemQuestion> Patches {get; set;}
}