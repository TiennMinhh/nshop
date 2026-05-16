using nShop.Catalog.Commands;

namespace nShop.Catalog.Api;

public partial class CatalogApi : CatalogService.CatalogServiceBase
{
    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        if (Guid.TryParse(request.CategoryId, out Guid categoryId) == false)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid CategoryId"));

        if (Guid.TryParse(request.TenantId, out Guid tenantId) == false)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TenantId"));

        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            categoryId,
            request.Price,
            request.Tags,
            request.Slug,
            request.Images,
            request.Groups.Select(g => Guid.Parse(g)).ToArray(),
            tenantId
        );

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return new CreateProductResponse
            {
                StatusCode = ServiceResultCodes.Error,
                ReasonPhrase = result.Errors.Any() ? string.Join(',', result.Errors.Select(e => e.ErrorMessage)) : "Unknown error"
            };
        }
        
        if (result.Value == null)
            throw new RpcException(new Status(StatusCode.Internal, "Invalid state: result.Value == null"));

        return new CreateProductResponse
        {
            Id = result.Value.ProductId.ToString(),
            StatusCode = ServiceResultCodes.Ok,
            ReasonPhrase = "OK"
        };
    }
}