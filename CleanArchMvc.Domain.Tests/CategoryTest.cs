using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Exceptions;
using CleanArchMvc.Exceptions.DomainValidation;

using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryTest
{
    [Fact]
    public void CreateCategory_WithValueParameters_ResultObjectValidState()
    {
        Action action = () =>
        {
            _ = new Category(1, "Category name");
        };

        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
        {
            _ = new Category(-1, "Category name");
        };

        action.Should().Throw<DomainExceptionValidation>().WithMessage(ResourceMessagesException.INVALID_ID);
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () =>
        {
            _ = new Category(1, "ca");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.NAME_TOO_SHORT);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void CreateCategory_InvalidName_DomainExceptionRequiredName(string name)
    {
        Action action = () =>
        {
            _ = new Category(1, name);
        };

        action.Should().Throw<DomainExceptionValidation>()
        .WithMessage(ResourceMessagesException.NAME_NULL_OR_EMPTY);
    }
}
