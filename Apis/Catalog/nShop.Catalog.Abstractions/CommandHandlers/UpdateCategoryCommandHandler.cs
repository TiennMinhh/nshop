using nShop.Catalog.DomainEvents;

namespace nShop.Catalog.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<UpdateCategoryResponse>>
    {
        private readonly IValidator<UpdateCategoryCommand> validator;
        private readonly IEventRepository eventRepository;
        private readonly IMediator mediator;

        public UpdateCategoryCommandHandler(IValidator<UpdateCategoryCommand> validator, IEventRepository eventRepository, IMediator mediator)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result<UpdateCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<UpdateCategoryResponse>.Failure(validationResult.Errors.Select(e => new Core.SeedWork.Results.ResultError("Validation", e.PropertyName)));
            }

            var category = await eventRepository.FindAsync<Category>(request.CategoryId, null, cancellationToken);
            if (category == null)
            {
                return Result<UpdateCategoryResponse>.NotFound();
            }

            category.Update(request.Name, request.Slug, request.ParentId);
            var result = await eventRepository.UpdateAsync(request.CategoryId, category, null, cancellationToken);

            foreach (var @event in category.PendingEvents)
            {
                await mediator.Publish((CategoryEvent)@event, cancellationToken);
            }

            //var categoryUpdatedIntegrationEvent = mapper.Map<Category, CategoryUpdatedIntegrationEvent>(category);

            //await eventBus.PublishAsync(categoryUpdatedIntegrationEvent, cancellationToken);

            return Result<UpdateCategoryResponse>.Success(new UpdateCategoryResponse
            {
                Id = category.ProductId,
                Name = category.Name,
                Slug = category.Slug,
                ParentId = category.ParentId,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            });
        }
    }
