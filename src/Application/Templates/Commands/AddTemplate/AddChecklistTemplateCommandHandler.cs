using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.AddTemplate;

public class AddChecklistTemplateCommandHandler : IRequestHandler<AddChecklistTemplateCommand, ItemTemplateDto>
{
    private readonly IItemTemplateRepository _itemTemplateRepository;

    private readonly IChecklistTemplateRepository _checklistTemplateRepository;

    // private readonly IQuestionTemplateRepository _questionTemplateRepository;



    public AddChecklistTemplateCommandHandler(IItemTemplateRepository itemTemplateRepository, IChecklistTemplateRepository checklistTemplateRepository)
    {
        _itemTemplateRepository = itemTemplateRepository;
        _checklistTemplateRepository = checklistTemplateRepository;
    }

    public async Task<ItemTemplateDto> Handle(AddChecklistTemplateCommand request, CancellationToken cancellationToken)
    {
        var itemTemplate = await _itemTemplateRepository.GetTemplateById(request.itemTemplateId, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemTemplate), request.itemTemplateId);

        var checklistTemplate = await _checklistTemplateRepository.CreateChecklistTemplate(itemTemplate.Id, cancellationToken);

        var questionTemplates = request.Questions.Select(q => QuestionTemplate.New(q)).ToList();

        checklistTemplate.SetQuestions(questionTemplates);
        // template.Questions = request?.Questions
        //     ?.Select(q => QuestionTemplate.New(q))
        //     ?.ToList() ?? [];

        await _checklistTemplateRepository.SaveChanges(cancellationToken);

        return itemTemplate.Adapt<ItemTemplateDto>();
    }

}
