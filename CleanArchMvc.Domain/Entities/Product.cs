using CleanArchMvc.Exceptions;
using CleanArchMvc.Exceptions.DomainValidation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Product : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product(string name, string description, decimal price, int stock, string image)
    {
        ValidateDomain(name, description, price, stock, image);
    }

    public Product(int id, string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(id < 0, ResourceMessagesException.INVALID_ID);
        Id = id;
        ValidateDomain(name, description, price, stock, image);
    }

    public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
    {
        ValidateDomain(name, description, price, stock, image);
        CategoryId = categoryId;
    }

    private void ValidateDomain(string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), ResourceMessagesException.NAME_NULL_OR_EMPTY);
        DomainExceptionValidation.When(name.Length < 3, ResourceMessagesException.NAME_TOO_SHORT);
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), ResourceMessagesException.DESCRIPTION_NULL_OR_EMPTY);
        DomainExceptionValidation.When(description.Length < 3, ResourceMessagesException.DESCRIPTION_TOO_SHORT);
        DomainExceptionValidation.When(price < 0, ResourceMessagesException.INVALID_PRICE);
        DomainExceptionValidation.When(stock < 0, ResourceMessagesException.INVALID_STOCK);
        DomainExceptionValidation.When(image?.Length > 250, ResourceMessagesException.IMAGE_NAME_TOO_LONG);

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image!;
    }
}
