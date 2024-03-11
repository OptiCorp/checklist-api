using Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
namespace Application.UnitTests;

public class Tests
{
    // [SetUp]
    // public void Setup()
    // {
    // }
    //TODO: copied the test from Jason Taylor, was just to validate that I had no issue setting up tests, not sure what specifically I should test for 

    [Fact]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Something", "something else"),
        };
        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Something" });
        actual["Something"].Should().BeEquivalentTo(new string[] { "something else" });
    }

    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Age", "must be 18 or older"),
                new ValidationFailure("Age", "must be 25 or younger"),
                new ValidationFailure("Password", "must contain at least 8 characters"),
                new ValidationFailure("Password", "must contain a digit"),
                new ValidationFailure("Password", "must contain upper case letter"),
                new ValidationFailure("Password", "must contain lower case letter"),
            };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

        actual["Age"].Should().BeEquivalentTo(new string[]
        {
                "must be 18 or older",
                "must be 25 or younger"
        });

        actual["Password"].Should().BeEquivalentTo(new string[]
        {
                "must contain lower case letter",
                "must contain upper case letter",
                "must contain at least 8 characters",
                "must contain a digit",
        });
    }


}