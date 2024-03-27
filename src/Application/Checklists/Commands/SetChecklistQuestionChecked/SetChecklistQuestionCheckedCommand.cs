using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Checklists.Commands;

public class SetChecklistQuestionCheckedCommand : IRequest
{
    public Guid checklistQuestionId {get; init;}

    public Guid checklistId {get; init;}

    public bool value {get; init;}

}