using System.ComponentModel.DataAnnotations.Schema;
using Application.Common;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Punches.Dtos;

public class PunchDto : DtoExtension, IRegister
{
    public required string Title { get; init; }

    public Guid ChecklistId { get; init; }

    public string Description { get; init; } = string.Empty;

    public string? SasToken {get; init;}

    public IEnumerable<Uri> PunchFileUris { get; init; } = [];
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Punch, PunchDto>()
            .Map(dest => dest.PunchFileUris, src => src.PunchFiles.Select(pf => pf.Uri)).Compile();
    }
}
