
using Application.Common.Interfaces;
using Application.Mobilizations.Commands;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Mobilizations.Commands;

public class AddMobilizationCommandHandler : IRequestHandler<AddMobilizationCommand, Guid>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly IChecklistRepository _checkListRepository;

    public AddMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checkListRepository = checklistRepository;
    }

    public async Task<Guid> Handle(AddMobilizationCommand request, CancellationToken cancellationToken)
    {
        var checklistId = await _checkListRepository.AddChecklist(new Checklist(), cancellationToken);

        var mobilization = MapToMobilization(request, checklistId);

        await _mobilizationRepository.AddMobilization(mobilization, cancellationToken);

        return mobilization.Id;
    }

    private static Mobilization MapToMobilization(AddMobilizationCommand request, Guid checklistId)
    {
        return new Mobilization
        (
            request.Title,
            checklistId,
            request.Description ?? string.Empty
        )
        {
            Created = DateOnly.FromDateTime(DateTime.Now),
            Status = MobilizationStatus.NotReady,
            Type = request.MobilizationType
            //MobilizationType mobilizationType, MobilizationStatus mobilizationStatus
        };
    }
}
