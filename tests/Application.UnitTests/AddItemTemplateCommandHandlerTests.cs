
using Application.Common.Interfaces;
using Application.Templates.AddTemplate;
using Domain.Entities;
using FluentAssertions;
using MobDeMob.Domain.ItemAggregate; 
using Moq;
using Xunit;

namespace Application.UnitTests;
public class AddItemTemplateCommandHandlerTests
{
    [Fact]
    public async Task Handle_GivenAddTemplateCommand_ShouldReturnItemTemplateId()
    {
        var id = "randomId";
        Mock<IItemReposiory> mockItemRepo = new Mock<IItemReposiory>();
        mockItemRepo.Setup(som => som.GetItemById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Item { Id = id });

        Mock<ITemplateRepository> mockTemplateRepo = new Mock<ITemplateRepository>();
        mockTemplateRepo.Setup(som => som.AddTemplate(It.IsAny<ItemTemplate>(), It.IsAny<CancellationToken>()));
        mockTemplateRepo.Setup(som => som.SaveChanges(It.IsAny<CancellationToken>()));



        var handler = new AddTemplateCommandHandler(mockTemplateRepo.Object, mockItemRepo.Object);
        var command = new AddTemplateCommand()
        {
            ItemId = "adas",
            Questions = ["something"]
        };

        //acts:
        
        var value = await handler.Handle(command, CancellationToken.None);

        //assertions:
        mockTemplateRepo.Verify(som => som.AddTemplate(It.IsAny<ItemTemplate>(), It.IsAny<CancellationToken>()), Times.Once());
        mockTemplateRepo.Verify(som => som.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        //Assert.Equal(id, idByEfCore);
    }

}