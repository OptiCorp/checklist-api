using MediatR;

namespace Application.Upload;


public class PunchUploadFile
{
   public required Stream Stream { get; set; }

   public required string FileName { get; set; }

   public required string ContentType { get; set; }
}

public class PunchUploadFilesCommand : IRequest
{
   public Guid Id { get; set; }
   // public required Stream Stream { get; set; }

   // public required string FileName { get; set; }

   // public required string ContentType { get; set; }
   public IEnumerable<PunchUploadFile> Files {get; set;} = [];
}
