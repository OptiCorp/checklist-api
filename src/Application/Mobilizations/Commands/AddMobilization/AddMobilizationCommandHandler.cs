
using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

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
        var checklistId = await _checkListRepository.AddChecklist(new Checklist(), cancellationToken);
        // if in the consctructor of Mobilization you create and assign a new checklist it will automatically add the checklist, so you wont need the logic above
        // for that to work you need to make sure the configs are correct. Something like:
        //  modelBuilder.Entity<Mobilization>()
        //.HasOne(s => s.Checklist)
        //.WithOne()
        //.HasForeignKey<Checklist>(x => x.Id);

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
