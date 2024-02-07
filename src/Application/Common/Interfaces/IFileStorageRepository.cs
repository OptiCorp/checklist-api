

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IFileStorageRepository
{
    // Task<string> CreateContainer(string containerName, CancellationToken cancellationToken);
    Task<string> UploadImage (Stream stream, string fileName, string containerName, string contentType, CancellationToken cancellationToken);
}