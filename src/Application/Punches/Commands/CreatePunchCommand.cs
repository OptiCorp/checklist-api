using MediatR;

namespace MobDeMob.Application.Punches.Commands;

public class CreatePunchCommand : IRequest<string>
{
    public required string Title {get; set;}
    public string? Description {get; set;}
    public required string ChecklistSectionId {get; set;}
}