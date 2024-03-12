using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.AddTemplate;

public class AddItemTemplateQuestionCommandHandler : IRequestHandler<AddItemTemplateQuestionCommand, Guid>
{
    private readonly IItemTemplateRepository _templateRepository;




    public AddItemTemplateQuestionCommandHandler(IItemTemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<Guid> Handle(AddItemTemplateQuestionCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetTemplateById(request.ItemTemplateId, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.ItemTemplateId); 

        var newQuestionTemplate = QuestionTemplate.New(request.Question);

        template.AddQuestionTemplate(newQuestionTemplate);

        await _templateRepository.SaveChanges(cancellationToken);

        return newQuestionTemplate.Id;        
    }

}
