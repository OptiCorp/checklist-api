using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Commands;

public class DeleteMobilizationCommandHandler : IRequestHandler<DeleteMobilizationCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistRepository _checklistRepository;


    public DeleteMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository;
    }
    public async Task Handle(DeleteMobilizationCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Mobilization), request.Id);

        mobilization.DeleteParts();

        await _checklistRepository.DeleteChecklist(mobilization.ChecklistId, cancellationToken);
    }
}
