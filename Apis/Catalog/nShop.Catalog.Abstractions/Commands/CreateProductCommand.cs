namespace nShop.Catalog.Commands;
public class CreateProductCommand : ProductCommandBase, IRequest<Result<CreateProductResponse>>, ITenancyEntity
{
    public double Price { get; set; }

    public CreateProductCommand()
    {
    }

    public CreateProductCommand(string productName, string description, Guid categoryId, double price, IEnumerable<string> tags, string slug, IEnumerable<string> images, IEnumerable<Guid> groups, Guid tenantId)
    {
        Name = productName;
        Description = description;
        CategoryId = categoryId;
        Price = price;
        Tags = tags;
        Slug = slug;
        Images = images;
        Groups = groups;
        TenantId = tenantId;
    }
}
