using nShop.Catalog.DomainEvents;

namespace nShop.Catalog.CommandHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryResponse>>
{
    private readonly IValidator<CreateCategoryCommand> validator;
    private readonly IEventRepository eventStore;
    private readonly IMediator mediator;

    public CreateCategoryCommandHandler(IValidator<CreateCategoryCommand> validator, IEventRepository eventStore, IMediator mediator)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    public async Task<Result<CreateCategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<CreateCategoryResponse>.Failure(validationResult.Errors.Select(e => new Core.SeedWork.Results.ResultError("Validation", e.PropertyName)));
        }
        
        var id = GuidHelpers.NewGuidVersion7();
        var category = Category.Create(id, Guid.Empty, command.Name, string.Empty);

        await eventStore.AddAsync(id, category, cancellationToken);

        foreach (var @event in category.PendingEvents)
        {
            await mediator.Publish((CategoryEvent)@event, cancellationToken);
        }

        category.ResetEvents();

        return Result<CreateCategoryResponse>.Success(new CreateCategoryResponse { Id = id });
    }
}