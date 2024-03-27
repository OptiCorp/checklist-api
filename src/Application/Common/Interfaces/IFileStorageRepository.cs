

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IFileStorageRepository
{
    // Task<string> CreateContainer(string containerName, CancellationToken cancellationToken);
    Task<Uri> UploadFile (Stream stream, string fileName, string containerName, string contentType, CancellationToken cancellationToken);

    Task<Uri> GenerateContainerSAS (string containerName, CancellationToken cancellationToken);

    string ConcatBlobUriWithContainerSasTokenUri (Uri blobUri, Uri containerSasUri);

}