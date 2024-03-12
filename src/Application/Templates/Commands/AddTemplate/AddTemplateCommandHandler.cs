using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.AddTemplate;

public class AddTemplateCommandHandler : IRequestHandler<AddTemplateCommand, Guid>
{
    private readonly IItemTemplateRepository _templateRepository;

    private readonly IQuestionTemplateRepository _questionTemplateRepository;

    private readonly IItemReposiory _itemReposiory;


    public AddTemplateCommandHandler(IItemTemplateRepository templateRepository, IItemReposiory itemReposiory)
    {
        _templateRepository = templateRepository;
        _itemReposiory = itemReposiory;
    }

    public async Task<Guid> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemReposiory.GetItemById(request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.ItemId);

        var template = MapToItemTemplate(item);

        await _templateRepository.AddTemplate(template, cancellationToken);

        template.Questions = request?.Questions
            ?.Select(q => QuestionTemplate.New(q))
            ?.ToList() ?? [];

        await _templateRepository.SaveChanges(cancellationToken);

        return template.Id;
    }

    private ItemTemplate MapToItemTemplate(Item item)
    {
        return new ItemTemplate(item.Id);
    }
}
