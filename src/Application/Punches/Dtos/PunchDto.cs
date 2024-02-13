using System.ComponentModel.DataAnnotations.Schema;
using Application.Common;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Punches.Dtos;

public class PunchDto : DtoExtension, IMapFrom<Punch>
{
    public required string Title { get; set; }

    public Guid ChecklistItemId { get; set; }

    public string? Description { get; set; }

    public IReadOnlyCollection<Uri> ImageBlobUris { get; set; } = [];

    public string? SASToken {get; set;}

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Punch, PunchDto>();
            //.ForMember(pd => pd.ImageBlobUris, opt => opt.MapFrom(p => p.ImageBlobUris));
    }
}
