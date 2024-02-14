namespace Api.Utilities;
public class FileModel
{
    public Guid id { get; set; }

    public IEnumerable<IFormFile> files {get; set;} = [];
    //public required IFormFile file { get; set; }
}
