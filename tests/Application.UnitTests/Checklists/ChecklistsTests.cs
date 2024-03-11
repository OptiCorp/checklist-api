
using Application.Checklists.Dtos;
using Application.Checklists.Queries;
using Application.Common.Interfaces;
using Application.Punches;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MobDeMob.Domain.ItemAggregate;
using Moq;

namespace Application.UnitTests.Checklists;

public class ChecklistTests
{
    [Fact]
    public async Task Given_ChecklistId_Should_GetQuestionsAndPunches()
    {
        var checklistId = Guid.NewGuid();
        var checklist = new Checklist(new ItemTemplate("someItemId"), checklistId);

        Mock<IChecklistRepository> mockChecklistRepo = new Mock<IChecklistRepository>();
        mockChecklistRepo.Setup(repo => repo.GetSingleChecklist(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(checklist);

        Mock<IChecklistQuestionRepository> mockChecklistQuestionsRepo = new Mock<IChecklistQuestionRepository>();
        mockChecklistQuestionsRepo.Setup(repo => repo.GetQuestionsByChecklistId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([new ChecklistQuestion(new QuestionTemplate(){Question = ""}, Guid.NewGuid())]);

        Mock<IPunchRepository> mockpunchRepo = new Mock<IPunchRepository>();
        mockpunchRepo.Setup(repo => repo.GetPunchesCount(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(3);

        var handler = new GetChecklistQueryHandler(mockChecklistRepo.Object, mockpunchRepo.Object, mockChecklistQuestionsRepo.Object);
        
        var query = new GetChecklistQuery()
        {
            ChecklistId = checklistId
        };

        //acts:
        var value = await handler.Handle(query, CancellationToken.None);
        var checklistDto = checklist.Adapt<ChecklistDto>();

        //assertions:
        mockChecklistRepo.Verify(rep => rep.GetSingleChecklist(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockChecklistQuestionsRepo.Verify(rep => rep.GetQuestionsByChecklistId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockpunchRepo.Verify(rep => rep.GetPunchesCount(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());

        Assert.Equal(checklist.Id, checklistDto.Id);
        
        
    }

}