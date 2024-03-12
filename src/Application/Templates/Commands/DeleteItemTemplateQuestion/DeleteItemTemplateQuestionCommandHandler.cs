using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.AddTemplate;

public class DeleteItemTemplateQuestionCommandHandler : IRequestHandler<DeleteItemTemplateQuestionCommand>
{

    private readonly IQuestionTemplateRepository _questionTemplateRepository;

    public DeleteItemTemplateQuestionCommandHandler(IQuestionTemplateRepository questionTemplateRepository)
    {
        _questionTemplateRepository = questionTemplateRepository;
    }

    public async Task Handle(DeleteItemTemplateQuestionCommand request, CancellationToken cancellationToken)
    {
        var questionTemplate = await _questionTemplateRepository.GetSingleQuestion(request.TemplateQuestionId, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.TemplateQuestionId); 

        await _questionTemplateRepository.DeleteQuestionById(questionTemplate.Id, cancellationToken); 
    }
}
