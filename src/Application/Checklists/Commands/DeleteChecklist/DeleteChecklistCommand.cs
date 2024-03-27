using MediatR;

namespace MobDeMob.Application.Checklists.Commands;

public class DeleteChecklistCommand : IRequest
{
    public Guid checklistId {get; init;}

    public string itemId {get; init;}
}