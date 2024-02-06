using MediatR;

namespace MobDeMob.Application.Checklists;

public class CreatePartChecklistQuestionsCommand : IRequest
{
    public required string partId {get; init;}
    public required IEnumerable<string> questions {get; init;}

}
