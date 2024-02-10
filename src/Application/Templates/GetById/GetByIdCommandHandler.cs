using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, ItemTemplate>
{
    private readonly ITemplateRepository _templateRepository;

    public GetByIdCommandHandler(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<ItemTemplate> Handle(GetByIdCommand request, CancellationToken cancellationToken)
    {
        return await _templateRepository.GetTemplateById(request.TemplateId, cancellationToken);
    }
}
