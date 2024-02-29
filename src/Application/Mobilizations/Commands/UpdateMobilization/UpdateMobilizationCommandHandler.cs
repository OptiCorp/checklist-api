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

        UpdateMobilization(mobilization, request);

        await _mobilizationRepository.SaveChanges(cancellationToken);
    }

    private static Mobilization UpdateMobilization(Mobilization mobilization, UpdateMobilizationCommand request)
    {
        mobilization.SetTitle(request.Title);
        mobilization.SetDescription(request.Description ?? string.Empty);
        mobilization.SetStatus(request.Status);

        return mobilization;
    }
}
