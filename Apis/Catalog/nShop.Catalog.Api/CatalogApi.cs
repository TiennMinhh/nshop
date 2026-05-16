using AutoMapper;
using MediatR;

namespace nShop.Catalog.Api;

public partial class CatalogApi : CatalogService.CatalogServiceBase
{
    private readonly ILogger<CatalogApi> logger;
    //private readonly IReadModelSyncFactory syncFactory;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public CatalogApi(
        //IReadModelSyncFactory syncFactory, 
        IMediator mediator, IMapper mapper, ILogger<CatalogApi> logger)
    {
        //this.syncFactory = syncFactory;
        this.mediator = mediator;
        this.mapper = mapper;
        this.logger = logger;
    }
}