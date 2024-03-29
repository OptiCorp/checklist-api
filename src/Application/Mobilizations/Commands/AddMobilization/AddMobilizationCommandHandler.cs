
using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Commands;

public class AddMobilizationCommandHandler : IRequestHandler<AddMobilizationCommand, string>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly IChecklistRepository _checkListRepository;

    public AddMobilizationCommandHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checkListRepository = checklistRepository;
    }
    public async Task<string> Handle(AddMobilizationCommand request, CancellationToken cancellationToken)
    {
        var checklistId = await _checkListRepository.AddChecklist(cancellationToken);
        var mobilization = MapToMobilization(request, checklistId);
        await _mobilizationRepository.AddMobilization(mobilization, cancellationToken);
        return mobilization.Id;
    }

    private static Mobilization MapToMobilization(AddMobilizationCommand request, string checklistId)
    {
        return new Mobilization
        {
            Title = request.Title,
            Description = request.Description,
            ChecklistId = checklistId,
            Type = request.MobilizationType
        };
    }
}