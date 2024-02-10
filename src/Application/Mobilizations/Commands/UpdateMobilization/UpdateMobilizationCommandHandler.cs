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
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.id);

        if (mobilization == null)
        {
            throw new Exception("Not ofund");// TODO: improve this exception
        }

        mobilization.Title = request.Title;
        mobilization.Description = request.Description;

        await _mobilizationRepository.SaveChanges();
    }
}
