using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Exceptions;
using CleanArchMvc.Exceptions.DomainValidation;

using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductTest
{
    [Fact]
    public void CreateProduct_WithValueParameters_ResultObjectValidState()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 10, 2, "Product Image");
        };

        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
        {
            _ = new Product(-1, "Product Name", "Product Description", 10, 2, "Product Image");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.INVALID_ID);
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () =>
        {
            _ = new Product(1, "pr", "Product Description", 10, 2, "Product Image");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.NAME_TOO_SHORT);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void CreateProduct_InvalidName_DomainExceptionRequiredName(string name)
    {
        Action action = () =>
        {
            _ = new Product(1, name, "Product Description", 10, 2, "Product Image");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.NAME_NULL_OR_EMPTY);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void CreateProduct_InvalidDescription_DomainExceptionRequiredDescription(string description)
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", description, 10, 2, "Product Image");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.DESCRIPTION_NULL_OR_EMPTY);
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 10, 2, "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.IMAGE_NAME_TOO_LONG);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void CreateProduct_InvalidImageName_NoDomainException(string imageName)
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 10, 2, imageName);
        };

        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_InvalidImageName_NoNullReferenceException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 10, 2, null!);
        };

        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainExceptionNegativeValue()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", -5, 2, "Image Product");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.INVALID_PRICE);
    }

    [Fact]
    public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 10, -5, "Image Product");
        };

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage(ResourceMessagesException.INVALID_STOCK);
    }
}
