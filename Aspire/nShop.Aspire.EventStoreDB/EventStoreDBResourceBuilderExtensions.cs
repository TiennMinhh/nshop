using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace nShop.Aspire.EventStoreDB;

public static class EventStoreDBResourceBuilderExtensions
    {
        public static IResourceBuilder<EventStoreDBResource> AddEventStore(
        this IDistributedApplicationBuilder builder,
        string name,
        int? httpPort = null,
        int? tcpPort = null)
        {
            // The AddResource method is a core API within .NET Aspire and is
            // used by resource developers to wrap a custom resource in an
            // IResourceBuilder<T> instance. Extension methods to customize
            // the resource (if any exist) target the builder interface.
            var resource = new EventStoreDBResource(name);

            return builder.AddResource(resource)
                          .WithImage(EventStoreDBContainerImageTags.Image)
                          .WithImageRegistry(EventStoreDBContainerImageTags.Registry)
                          .WithImageTag(EventStoreDBContainerImageTags.Tag)
                          .WithEndpoint(port: tcpPort, targetPort: EventStoreDBResource.DefaultTcpPort, name: EventStoreDBResource.TcpEndpointName)
                          .WithHttpEndpoint(port: httpPort, targetPort: EventStoreDBResource.DefaultHttpPort, name: EventStoreDBResource.HttpEndpointName)
                          .WithEnvironment(ConfigureEventStoreDBContainer);
        }

        private static void ConfigureEventStoreDBContainer(EnvironmentCallbackContext context)
        {
            context.EnvironmentVariables.Add("EVENTSTORE_CLUSTER_SIZE", "1");
            context.EnvironmentVariables.Add("EVENTSTORE_RUN_PROJECTIONS", "All");
            context.EnvironmentVariables.Add("EVENTSTORE_START_STANDARD_PROJECTIONS", "true");
            context.EnvironmentVariables.Add("EVENTSTORE_EXT_TCP_PORT", $"{EventStoreDBResource.DefaultTcpPort}");
            context.EnvironmentVariables.Add("EVENTSTORE_HTTP_PORT", $"{EventStoreDBResource.DefaultHttpPort}");
            context.EnvironmentVariables.Add("EVENTSTORE_INSECURE", "true");
            //context.EnvironmentVariables.Add("EVENTSTORE_DEV", "true");
            context.EnvironmentVariables.Add("EVENTSTORE_ENABLE_ATOM_PUB_OVER_HTTP", "true");
            context.EnvironmentVariables.Add("EVENTSTORE_ENABLE_EXTERNAL_TCP", "true");
        }

        public static IResourceBuilder<EventStoreDBResource> WithDataVolume(this IResourceBuilder<EventStoreDBResource> builder)
        {
            return builder
                .WithVolume("eventstore-volume-data", "/var/lib/eventstore-data");
        }
        public static IResourceBuilder<EventStoreDBResource> WithLogsVolume(this IResourceBuilder<EventStoreDBResource> builder)
        {
            return builder
                .WithVolume("eventstore-volume-logs", "/var/log/eventstore")
                ;
        }
    }

    internal static class EventStoreDBContainerImageTags
    {
        internal const string Registry = "docker.io";

        internal const string Image = "eventstore/eventstore";

        internal const string Tag = "23.10.1-jammy";
    }