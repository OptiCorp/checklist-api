using MediatR;

namespace Application.Upload;

public class PunchUploadFileCommand : IRequest
{
   public Guid Id {get; set;}
   public required Stream Stream {get; set;}

   public required string FileName {get; set;}

   public required string ContentType {get; set;} 
}
