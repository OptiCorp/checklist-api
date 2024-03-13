
using Application.Common.Interfaces;
using Application.Templates.Queries;
using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;
using Moq;

namespace Application.UnitTests.Templates;

public class CheckConflictsOnUpdateItemTemplateQuestionsQueryHandlerTests
{
    private readonly Mock<IChecklistRepository> _mockChecklistRepository;

    private readonly CheckConflictsOnUpdateItemTemplateQuestionsQueryHandler _handler;

    public CheckConflictsOnUpdateItemTemplateQuestionsQueryHandlerTests()
    {
        _mockChecklistRepository = new Mock<IChecklistRepository>();
        _handler = new CheckConflictsOnUpdateItemTemplateQuestionsQueryHandler(_mockChecklistRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenChecklistStatusesIsNotNotStarted_ShouldReturnListOfChecklistIds()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var id3 = Guid.NewGuid();

        var checklistCollectionId = Guid.NewGuid();

        var checklistMock1 = new Checklist(new ItemTemplate(Guid.NewGuid().ToString()){}, checklistCollectionId).SetChecklistStatus(ChecklistStatus.NotStarted);
        var checklistMock2 = new Checklist(new ItemTemplate(Guid.NewGuid().ToString()){}, checklistCollectionId).SetChecklistStatus(ChecklistStatus.InProgress);
        var checklistMock3 = new Checklist(new ItemTemplate(Guid.NewGuid().ToString()){}, checklistCollectionId).SetChecklistStatus(ChecklistStatus.Completed);


        checklistMock1.Id = id1;
        checklistMock2.Id = id2;
        checklistMock3.Id = id3;


        IEnumerable<Checklist> mockChecklists = [
            checklistMock1,
            checklistMock2,
            checklistMock3
            ];
        _mockChecklistRepository.Setup(repo => repo.GetChecklistsByItemTemplateId(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockChecklists);

        var query = new CheckConflictsOnUpdateItemTemplateQuestionsQuery()
        {
            ItemTemplateId = Guid.NewGuid()
        };

        //acts:
        var checklistIds = await _handler.Handle(query, CancellationToken.None);

        //asserts:
        _mockChecklistRepository.Verify(som => som.GetChecklistsByItemTemplateId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal([id2, id3], checklistIds);
    }
}