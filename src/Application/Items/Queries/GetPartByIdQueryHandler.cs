
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Items.Queries;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Part?>
{
    private readonly IItemsRepository _itemsRepository;

    public GetItemByIdQueryHandler(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }
    public async Task<Part?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await _itemsRepository.GetById(request.Id);
    }
}