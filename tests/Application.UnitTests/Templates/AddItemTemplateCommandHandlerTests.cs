
using Application.Common.Interfaces;
using Application.Templates.AddTemplate;
using Domain.Entities;
using Moq;
using Xunit;

public class AddItemTemplateCommandHandlerTests
{
    [Fact]
    public void somethingTest()
    {
        Mock<IItemReposiory> mockItemRepo = new Mock<IItemReposiory>();
        mockItemRepo.Setup(som => som.GetItemById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Item { Id = "a" });

        var handler = new AddTemplateCommandHandler()
    }

}