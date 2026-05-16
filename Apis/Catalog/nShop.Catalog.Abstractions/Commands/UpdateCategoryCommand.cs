namespace nShop.Catalog.Commands;

public class UpdateCategoryCommand : IRequest<Result<UpdateCategoryResponse>>, ITenancyEntity

{
    public Guid CategoryId { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public Guid TenantId { get; set; }

    public UpdateCategoryCommand()
    {
    }

    public UpdateCategoryCommand(Guid categoryId, Guid parentId, string name, string slug)
    {
        CategoryId = categoryId;
        ParentId = parentId;
        Name = name;
        Slug = slug;
    }
}
