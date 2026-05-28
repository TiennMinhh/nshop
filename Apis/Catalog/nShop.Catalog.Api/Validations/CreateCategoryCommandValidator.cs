using nShop.Catalog.Api.Handlers.Commands;

namespace nShop.Catalog.Api.Validations;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}