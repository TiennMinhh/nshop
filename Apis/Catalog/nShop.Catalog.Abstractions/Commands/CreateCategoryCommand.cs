namespace nShop.Catalog.Commands;

public class CreateCategoryCommand : IRequest<Result<CreateCategoryResponse>>, ITenancyEntity
{
    public Guid? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public Guid TenantId { get; set; }

    public CreateCategoryCommand()
    {
    }

    public CreateCategoryCommand(Guid tenantId, Guid? parentId, string name, string slug)
    {
        TenantId = tenantId;
        ParentId = parentId;
        Name = name;
        Slug = slug;
    }
}