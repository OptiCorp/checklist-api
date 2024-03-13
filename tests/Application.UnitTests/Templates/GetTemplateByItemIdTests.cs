
using Application.Common.Interfaces;
using Application.Templates;
using Application.Templates.GetById;
using Domain.Entities;
using MobDeMob.Domain.ItemAggregate;
using Moq;

namespace Application.UnitTests.Templates;

public class GetTemplateByItemIdTests
{
    private readonly Mock<IItemTemplateRepository> _mockIItemTemplateRepository;

    private readonly Mock<IItemReposiory> _mockIItemRepository;

    private readonly GetTemplateQueryHandler _handler;

    public GetTemplateByItemIdTests()
    {
        _mockIItemTemplateRepository = new Mock<IItemTemplateRepository>();

        _mockIItemRepository = new Mock<IItemReposiory>();

        _handler = new GetTemplateQueryHandler(_mockIItemTemplateRepository.Object, _mockIItemRepository.Object);
    }

    [Fact]
    public async Task Handle_GivenItemId_ShouldReturnItemTemplateAsDto()
    {
        var itemId = Guid.NewGuid().ToString();
        var mockItem = new Item() { Id = itemId };
        var mockItemTemplate = new ItemTemplate(itemId)
        {
            Id = Guid.NewGuid()
        };

        _mockIItemRepository.Setup(repo => repo.GetItemById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockItem);

        _mockIItemTemplateRepository.Setup(repo => repo.GetTemplateByItemId(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockItemTemplate);

        var query = new GetTemplateQuery()
        {
            ItemId = itemId
        };

        var itemTemplate = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(itemTemplate);
        Assert.IsType<ItemTemplateDto>(itemTemplate);
        Assert.Equal(mockItemTemplate.Id, itemTemplate.Id);
    }


    [Fact]
    public async Task Handle_GivenItemIdWithNoItemTemplate_ShouldReturnNull()
    {
        var itemId = Guid.NewGuid().ToString();
        var mockItem = new Item() { Id = itemId };
        var mockItemTemplate = new ItemTemplate(itemId)
        {
            Id = Guid.NewGuid()
        };

        _mockIItemRepository.Setup(repo => repo.GetItemById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockItem);

        _mockIItemTemplateRepository.Setup(repo => repo.GetTemplateByItemId(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((ItemTemplate)null);

        var query = new GetTemplateQuery()
        {
            ItemId = itemId
        };

        var itemTemplate = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(itemTemplate);
    }

}