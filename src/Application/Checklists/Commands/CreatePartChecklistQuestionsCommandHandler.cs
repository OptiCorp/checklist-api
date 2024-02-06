
using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Checklists;

public class CreatePartChecklistQuestionsCommandHandler : IRequestHandler<CreatePartChecklistQuestionsCommand>
{
    private readonly IChecklistRepository _checkListRepository;

    public CreatePartChecklistQuestionsCommandHandler(IChecklistRepository checklistRepository)
    {
        _checkListRepository = checklistRepository;
    }

    public async Task Handle(CreatePartChecklistQuestionsCommand request, CancellationToken cancellationToken)
    {
        await _checkListRepository.CreatePartChecklistQuestions(request.partId, request.questions, cancellationToken);
    }
}