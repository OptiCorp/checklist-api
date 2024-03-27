
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
    private readonly IChecklistCollectionRepository _checklistCollectionRepository;

    public AddMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistCollectionRepository checklistCollectionRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistCollectionRepository = checklistCollectionRepository;
    }

    public async Task<Guid> Handle(AddMobilizationCommand request, CancellationToken cancellationToken)
    {
        var checklistCollectionId = await _checklistCollectionRepository.AddChecklistCollection(new ChecklistCollection(), cancellationToken);

        var mobilization = MapToMobilization(request, checklistCollectionId);

        await _mobilizationRepository.AddMobilization(mobilization, cancellationToken);

        return mobilization.Id;
    }

    private static Mobilization MapToMobilization(AddMobilizationCommand request, Guid checklistCollectionId)
    {
        var mob = Mobilization.New(request.Title, request.MobilizationType, MobilizationStatus.NotReady, checklistCollectionId, request.Description);
        return mob;
    }
}
