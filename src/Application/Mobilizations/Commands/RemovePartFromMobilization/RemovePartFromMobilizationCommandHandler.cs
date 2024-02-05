using Application.Common.Interfaces;
using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommandHandler : IRequestHandler<RemovePartFromMobilizationCommand>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    public RemovePartFromMobilizationCommandHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task Handle(RemovePartFromMobilizationCommand request, CancellationToken cancellationToken)
    {
        await _mobilizationRepository.RemovePartFromMobilization(request.id, request.partId, cancellationToken);
    }
}
