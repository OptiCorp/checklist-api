using Application.Checklists.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Commands;

public class SetChecklistStatusCommand : IRequest
{
    public Guid ChecklistId {get; init;}

    public ChecklistStatus Status {get; init;}

}