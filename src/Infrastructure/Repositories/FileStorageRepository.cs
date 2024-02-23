
using System.Net.Mime;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.VisualBasic;
using MobDeMob.Application.Common.Interfaces;

namespace MobDeMob.Infrastructure.Repositories;

public class FileStorageRepositories : IFileStorageRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    public FileStorageRepositories(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }


    private async Task<UserDelegationKey> RequestUserDelegationKey()
    {
        // Get a user delegation key for the Blob service that's valid for 1 day
        UserDelegationKey userDelegationKey =
            await _blobServiceClient.GetUserDelegationKeyAsync(
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow.AddDays(1));

        return userDelegationKey;
    }

    private static Uri CreateUserDelegationSASContainer(
    BlobContainerClient containerClient,
    UserDelegationKey userDelegationKey)
    {
        // Create a SAS token for the container resource that's also valid for 1 day
        BlobSasBuilder sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = containerClient.Name,
            Resource = "c",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(1).AddHours(1)
        };

        // Specify the necessary permissions
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        // Add the SAS token to the blob URI
        BlobUriBuilder uriBuilder = new BlobUriBuilder(containerClient.Uri)
        {
            // Specify the user delegation key
            Sas = sasBuilder.ToSasQueryParameters(
                userDelegationKey,
                containerClient.GetParentBlobServiceClient().AccountName)
        };

        return uriBuilder.ToUri();
        //return uriBuilder;
    }

    private async Task<BlobContainerClient> GetContainer(string containerName, CancellationToken cancellationToken)
    {
        BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);
        return container;
    }


    private static string CreateBlobName(Guid fileId, string fileName)
    {
        var fileExt = fileName.Split(".").LastOrDefault();
        return $"{fileId}.{fileExt}".ToLower();
    }

    public async Task<Uri> UploadImage(Stream stream, string fileName, string containerName, string contentType, CancellationToken cancellationToken)
    {
        BlobContainerClient container = await GetContainer(containerName, cancellationToken);
        var fileId = Guid.NewGuid();
        string blobName = CreateBlobName(fileId, fileName);

        var blobInstance = container.GetBlobClient(blobName);

        await blobInstance.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType,
            }
        });

        return blobInstance.Uri;
    }

    public async Task<Uri> GenerateContainerSAS(string containerName, CancellationToken cancellationToken)
    {
        BlobContainerClient blobContainerClient = await GetContainer(containerName, cancellationToken);
        var userDelegationKey = await RequestUserDelegationKey();
        Uri containerSASURI = CreateUserDelegationSASContainer(blobContainerClient, userDelegationKey);
        return containerSASURI;
    }

    public string ConcatBlobUriWithContainerSasTokenUri (Uri blobUri, Uri containerSasUri)
    {
        var sasToken = containerSasUri.Query;
    
        // Concatenate the blob URI with the SAS token
        var blobUriWithSas = new Uri(blobUri + sasToken);
    
        return blobUriWithSas.ToString();
    }
}