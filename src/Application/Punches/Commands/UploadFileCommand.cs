using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class UploadFileCommand : IRequest<string>
{
    public required Stream Stream {get; set;}

    public required string FileName {get; set;}

    public required string ContainerName {get; set;}
    public required string ContentType {get; set;} 
}