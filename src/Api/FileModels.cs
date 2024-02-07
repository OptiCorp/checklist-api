namespace Api.Models;
public class FileModel
{
    public required IFormFile file{get; set;}

    public required string ContainerName {get; set;}
}