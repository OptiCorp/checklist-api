using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;

namespace MobDeMob.Application.Mobilizations.Commands;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
{
    private readonly IFileStorageRepository _fileStorageRepository;

    public UploadFileCommandHandler(IFileStorageRepository fileStorageRepository)
    {
        _fileStorageRepository = fileStorageRepository;
    }

    public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var absUri = await _fileStorageRepository.UploadImage(request.Stream, request.FileName, request.ContainerName, request.ContentType, cancellationToken);
        return absUri;
    }
}
