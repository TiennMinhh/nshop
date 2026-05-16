namespace nShop.Catalog.Commands;

public class ProductCommandBase : ITenancyEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public IEnumerable<string> Tags { get; set; } = [];
    public string Slug { get; set; } = string.Empty;
    public IEnumerable<string> Images { get; set; } = []; 
    public IEnumerable<Guid> Groups { get; set; } = []; 
    public Guid TenantId { get; set; }

    public ProductCommandBase()
    {
    }

    public ProductCommandBase(string productName, string description, Guid categoryId, IEnumerable<string> tags, string slug, IEnumerable<string> images, IEnumerable<Guid> groups)
    {
        Name = productName;
        Description = description;
        CategoryId = categoryId;
        Tags = tags;
        Slug = slug;
        Images = images;
        Groups = groups;
    }
}
