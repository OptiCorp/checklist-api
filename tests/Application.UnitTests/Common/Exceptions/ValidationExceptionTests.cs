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

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Something", "something else"),
        };
        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] {"Something"});
        actual["Something"].Should().BeEquivalentTo(new string[] {"something else"});
    }
}