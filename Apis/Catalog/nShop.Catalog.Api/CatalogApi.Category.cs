using nShop.Catalog.Api.Handlers.Commands;

namespace nShop.Catalog.Api;

public partial class CatalogApi : CatalogService.CatalogServiceBase
{
    public override async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request,
        ServerCallContext context)
    {
        if (Guid.TryParse(request.TenantId, out Guid tenantId) == false)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TenantId"));
        if (Guid.TryParse(request.ParentId, out Guid parentId) == false)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ParentId"));

        var command = new CreateCategoryCommand(
            tenantId,
            parentId,
            request.Name,
            request.Slug
        );

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return new CreateCategoryResponse
            {
                StatusCode = ServiceResultCodes.Error,
                ReasonPhrase = result.Errors.Any()
                    ? string.Join(',', result.Errors.Select(e => e.ErrorMessage))
                    : "Unknown error"
            };
        }

        if (result.Value == null)
            throw new RpcException(new Status(StatusCode.Internal, "Invalid state: result.Value == null"));

        return new CreateCategoryResponse
        {
            Id = result.Value.Id.ToString(),
            StatusCode = ServiceResultCodes.Ok,
            ReasonPhrase = "OK"
        };
    }

    public override async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest request,
        ServerCallContext context)
    {
        if (Guid.TryParse(request.CategoryId, out Guid categoryId) == false)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid CategoryId"));
        if (Guid.TryParse(request.ParentId, out Guid parentId) == false)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ParentId"));

        var command = new UpdateCategoryCommand(
            categoryId,
            parentId,
            request.Name,
            request.Slug
        );

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return new UpdateCategoryResponse
            {
                StatusCode = ServiceResultCodes.Error,
                ReasonPhrase = result.Errors.Any()
                    ? string.Join(',', result.Errors.Select(e => e.ErrorMessage))
                    : "Unknown error"
            };
        }

        if (result.Value == null)
            throw new RpcException(new Status(StatusCode.Internal, "Invalid state: result.Value == null"));

        return new UpdateCategoryResponse
        {
            Id = result.Value.Id.ToString(),
            StatusCode = ServiceResultCodes.Ok,
            ReasonPhrase = "OK"
        };
    }
}