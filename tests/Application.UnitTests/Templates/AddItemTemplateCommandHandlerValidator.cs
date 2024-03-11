

using Application.Common.Interfaces;
using Application.Templates.AddTemplate;
using FluentValidation.TestHelper;
using MobDeMob.Application.Templates.Validators;
using Moq;

namespace Application.UnitTests.Templates;

public class AddItemTemplateCommandHandlerValidator
{

    [Fact]
    public async Task Handle_GivenAddTemplateCommandWithEmptyItemId_ShouldRaiseException()
    {
        var validator = new AddTemplateCommandValidator();
        var model = new AddTemplateCommand(){ItemId = "", Questions = []};
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(atc => atc.ItemId).WithErrorMessage("itemId cannot be empty");
    }
}