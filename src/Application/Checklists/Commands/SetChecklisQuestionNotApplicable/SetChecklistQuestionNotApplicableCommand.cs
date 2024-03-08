using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Checklists.Commands;

public class SetChecklistQuestionNotApplicableCommand : IRequest
{
    public Guid ChecklistQuestionId {get; set;}

    public Guid ChecklistId {get; init;}

    public bool Value {get; set;}

}