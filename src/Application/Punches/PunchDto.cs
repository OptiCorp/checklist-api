using System.ComponentModel.DataAnnotations.Schema;
using Application.Common;
using Application.Common.Mappings;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Punches.Dtos;

public class PunchDto : DtoExtension
{
    public required string Title { get; init; }

    public Guid ChecklistItemId { get; init; }

    public string? Description { get; init; }

    public string? SasToken {get; init;}

    public IReadOnlyCollection<Uri> ImageBlobUris { get; init; } = [];

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<Punch, PunchDto>()
    //         .ForMember(pd => pd.ImageBlobUris, opt => opt.MapFrom(p => p.ImageBlobUris));
    // }
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Punch, PunchDto>();
    }
}
