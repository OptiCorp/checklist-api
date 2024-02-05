using Application.Common.Interfaces;
using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class AddPartToMobilizationCommandHandler : IRequestHandler<AddPartToMobilizationCommand>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    public AddPartToMobilizationCommandHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task Handle(AddPartToMobilizationCommand request, CancellationToken cancellationToken)
    {
        await _mobilizationRepository.AddPartToMobilization(request.id, request.partId, cancellationToken);
    }
}
