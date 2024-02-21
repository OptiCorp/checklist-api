using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Domain.Entities;

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
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.id, cancellationToken) ?? throw new NotFoundException(nameof(Mobilization), request.id);

        ChangeMobilization(mobilization, request);

        await _mobilizationRepository.SaveChanges(cancellationToken);
    }

    //TODO: Not sure if the frontend should be allowed to send Description for example, as null. If so should the description be updated or not?
    private static Mobilization ChangeMobilization(Mobilization mobilization, UpdateMobilizationCommand request)
    {
        mobilization.Title = request.Title;
        mobilization.Description = request.Description;
        mobilization.Type = request.Type;
        mobilization.Status = request.Status;
        mobilization.LastModified = DateTime.Now;

        return mobilization;
    }
}
