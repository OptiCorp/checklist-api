
using Application.Common.Interfaces;
using Application.Templates.UpdateTemplate;
using Domain.Entities.TemplateAggregate;
using Moq;

namespace Application.UnitTests.Templates;

public class UpdateTemplateCommandHandlerTests
{

    private readonly Mock<IQuestionTemplateRepository> _mockQuestionTemplateRepository;
    private readonly UpdateTemplateCommandHandler _handler;


    public UpdateTemplateCommandHandlerTests()
    {
        _mockQuestionTemplateRepository = new Mock<IQuestionTemplateRepository>();
        _handler = new UpdateTemplateCommandHandler(_mockQuestionTemplateRepository.Object);
    }

    [Fact]
    public async Task Handle_GivenNewQuestionStringAndQuestionTemplateId_ShouldUpdateQuestion()
    {
        var questionTemplateId = Guid.NewGuid();
        var questionTemplate = QuestionTemplate.New("first question!");
        questionTemplate.Id = questionTemplateId;

        _mockQuestionTemplateRepository.Setup(repo => repo.GetSingleQuestion(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(questionTemplate);
        _mockQuestionTemplateRepository.Setup(som => som.SaveChanges(It.IsAny<CancellationToken>()));

        
        var command = new UpdateTemplateCommand()
        {
            Question = "Update the question!",
            QuestionTemplateId = Guid.NewGuid()
        };

        //acts:
        await _handler.Handle(command, CancellationToken.None);

        //assertions:
        _mockQuestionTemplateRepository.Verify(som => som.GetSingleQuestion(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        
        _mockQuestionTemplateRepository.Verify(som => som.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task Handle_GivenSameQuestionStringAndQuestionTemplateId_ShouldNotUpdateQuestion()
    {
        var questionTemplateId = Guid.NewGuid();
        var questionTemplate = QuestionTemplate.New("first question!");
        questionTemplate.Id = questionTemplateId;

        _mockQuestionTemplateRepository.Setup(repo => repo.GetSingleQuestion(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(questionTemplate);
        _mockQuestionTemplateRepository.Setup(som => som.SaveChanges(It.IsAny<CancellationToken>()));

     

        var command = new UpdateTemplateCommand()
        {
            Question = "first question!",
            QuestionTemplateId = Guid.NewGuid()
        };

        //acts
        await _handler.Handle(command, CancellationToken.None);

        //assertions:
        _mockQuestionTemplateRepository.Verify(som => som.GetSingleQuestion(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        _mockQuestionTemplateRepository.Verify(som => som.SaveChanges(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_GivenNewQuestionString_ShouldUpdateQuestion()
    {
        var questionTemplate = QuestionTemplate.New("first question!");

        _mockQuestionTemplateRepository.Setup(repo => repo.GetSingleQuestion(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(questionTemplate);
        _mockQuestionTemplateRepository.Setup(som => som.SaveChanges(It.IsAny<CancellationToken>()));

        
        var command = new UpdateTemplateCommand()
        {
            Question = "Update the question!",
            QuestionTemplateId = Guid.NewGuid()
        };

        //acts:
        await _handler.Handle(command, CancellationToken.None);

        //assertions:
        Assert.Equal(command.Question, questionTemplate.Question);
        _mockQuestionTemplateRepository.Verify(som => som.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
    }
}