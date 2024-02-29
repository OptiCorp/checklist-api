using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Commands;

public class DeleteMobilizationCommandHandler : IRequestHandler<DeleteMobilizationCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistCollectionRepository _checklistCollectionRepository;


    public DeleteMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistCollectionRepository checklistCollectionRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistCollectionRepository = checklistCollectionRepository;
    }
    public async Task Handle(DeleteMobilizationCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.Id, cancellationToken) 
            ?? throw new NotFoundException(nameof(Mobilization), request.Id);

        //mobilization.DeleteParts(); //TODO this shouldt be neccessasy since the dabase handles cascade delete

        await _checklistCollectionRepository.DeleteChecklistCollection(mobilization.ChecklistCollectionId, cancellationToken);
    }
}
