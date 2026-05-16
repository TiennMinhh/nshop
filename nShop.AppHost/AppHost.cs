using nShop.Aspire.EventStoreDB;

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("pg-username", secret: true);
var password = builder.AddParameter("pg-password", secret: true);

var cache = builder.AddRedis("cache");
var postgres = builder.AddPostgres("catalog-db-server", username, password).WithDataVolume();
var eventStore = builder.AddEventStore("eventstore")
    .WithDataVolume()
    .WithLogsVolume();

var kafka = builder.AddKafka("event-bus").WithKafkaUI();

var catalogWriteDb = postgres.AddDatabase("catalog-db");
var catalogReadDb = catalogWriteDb; // postgres.AddDatabase("catalog-db");

var mongoDb = builder.AddMongoDB("catalog-mongo-db").WithDataVolume(); // here we use MongoDB for both read/write model, but we can use different databases using replicas

var catalogApi =  builder.AddProject<Projects.nShop_Catalog_Api>("catalog-api")
    .WithReference(cache)
    .WithReference(catalogReadDb)
    .WithReference(kafka)
    .WithReference(mongoDb)
    .WithReference(eventStore);

builder.Build().Run();