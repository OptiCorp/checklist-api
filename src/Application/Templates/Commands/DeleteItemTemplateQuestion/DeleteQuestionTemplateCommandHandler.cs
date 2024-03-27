using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Commands;

public class DeleteQuestionTemplateCommandHandler : IRequestHandler<DeleteQuestionTemplateCommand>
{

    private readonly IQuestionTemplateRepository _questionTemplateRepository;

    public DeleteQuestionTemplateCommandHandler(IQuestionTemplateRepository questionTemplateRepository)
    {
        _questionTemplateRepository = questionTemplateRepository;
    }

    public async Task Handle(DeleteQuestionTemplateCommand request, CancellationToken cancellationToken)
    {
        var questionTemplate = await _questionTemplateRepository.GetSingleQuestion(request.QuestionTemplateId, cancellationToken)
            ?? throw new NotFoundException(nameof(QuestionTemplate), request.QuestionTemplateId); 

        await _questionTemplateRepository.DeleteQuestionById(questionTemplate.Id, cancellationToken); 
    }
}
