using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommandHandler : IRequestHandler<RemovePartFromMobilizationCommand>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly IPartsRepository _partsRepository;

    public RemovePartFromMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IPartsRepository partsRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _partsRepository = partsRepository;
    }

    public async Task Handle(RemovePartFromMobilizationCommand request, CancellationToken cancellationToken)
    {
        var part = await _partsRepository.GetById(request.partId, cancellationToken)
            ?? throw new Exception("Part not found"); // TODO: improve exception
        var mobilization = await _mobilizationRepository.GetById(request.id, cancellationToken)
            ?? throw new Exception($"Mobilizaiton with id: {request.id} does not have any Checklist associated"); // same as above

        mobilization.RemovePartToMobilization(part);

        await _partsRepository.Delete(part);
    }
}
