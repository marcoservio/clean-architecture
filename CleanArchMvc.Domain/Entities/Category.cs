using CleanArchMvc.Exceptions;
using CleanArchMvc.Exceptions.DomainValidation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category : EntityBase
{
    public string Name { get; private set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = [];

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, ResourceMessagesException.INVALID_ID);
        Id = id;
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), ResourceMessagesException.NAME_NULL_OR_EMPTY);
        DomainExceptionValidation.When(name.Length < 3, ResourceMessagesException.NAME_TOO_SHORT);

        Name = name;
    }
}
