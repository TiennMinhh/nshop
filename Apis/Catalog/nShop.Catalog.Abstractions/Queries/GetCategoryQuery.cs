namespace nShop.Catalog.Queries
{
    public class GetCategoryQuery : IRequest<Result<GetCategoryResponse>>
    {
        public Guid CategoryId { get; set; }
    }
}
