namespace Api.Models;
public class FileModel
{
    public required string id {get; set;}
    public required IFormFile file{get; set;}
}