using Application.Common.Interfaces;
using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class DeleteMobilizationCommandHandler : IRequestHandler<DeleteMobilizationCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;

    public DeleteMobilizationCommandHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task Handle(DeleteMobilizationCommand request, CancellationToken cancellationToken)
    {
        await _mobilizationRepository.DeleteMobilization(request.id, cancellationToken);
    }
}
