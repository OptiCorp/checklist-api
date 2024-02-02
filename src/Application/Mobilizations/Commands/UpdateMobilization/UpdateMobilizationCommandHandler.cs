using Application.Common.Interfaces;
using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class UpdateMobilizationCommandHandler : IRequestHandler<UpdateMobilizationCommand>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    public UpdateMobilizationCommandHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task Handle(UpdateMobilizationCommand request, CancellationToken cancellationToken)
    {
        await _mobilizationRepository.UpdateMobilization(request.id, request.Title, request.Description, cancellationToken);
    }
}
