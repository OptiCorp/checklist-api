using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Checklists.Commands;
public class SetChecklistItemQuestionPatchNotApplicableCommandRequest
{
    public Guid Id {get; set;} 

    public required JsonPatchDocument<ChecklistItemQuestion> Patches {get; set;}
}

public class SetChecklistItemQuestionPatchNotApplicableCommand : IRequest
{
    public Guid Id {get; set;} 

    public required JsonPatchDocument<ChecklistItemQuestion> Patches {get; set;}

    public required ModelStateDictionary ModelState {get; init;}

}