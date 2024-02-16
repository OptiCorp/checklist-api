using Application.Common.Exceptions;
using Application.Punches;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Punches.Commands;

public class UpdatePunchCommandHandler : IRequestHandler<UpdatePunchCommand>
{
    private readonly IPunchRepository _punchRepository;


    public UpdatePunchCommandHandler(IPunchRepository punchRepository)
    {
        _punchRepository = punchRepository;
    }

    public async Task Handle(UpdatePunchCommand request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunch(request.Id) 
            ?? throw new NotFoundException(nameof(Punch), request.Id);

        ChangePunch(punch, request);

        await _punchRepository.SaveChanges(cancellationToken);
    }

    private static Punch ChangePunch(Punch punch, UpdatePunchCommand updatePunchCommand)
    {
        punch.Title = updatePunchCommand.Title;
        punch.Description = updatePunchCommand.Description;

        return punch;
    }
}
