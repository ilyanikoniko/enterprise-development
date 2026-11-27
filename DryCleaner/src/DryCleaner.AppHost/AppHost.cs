using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

const string databaseName = "drycleanerdb";

var isTesting = builder.Configuration.GetValue(
    "IntegrationTesting", false);

var password = builder.AddParameter("MySqlPassword", secret: true);

var mysqlServer = builder.AddMySql("mysql", password: password);

if (!isTesting)
{
    mysqlServer.WithDataVolume();
}

var mysql = mysqlServer.AddDatabase(databaseName);

var generator = builder
    .AddProject<Projects.DryCleaner_Generator_Grpc_Host>(
        "ordergenerator",
        launchProfileName: "https");

var api = builder.AddProject<Projects.DryCleaner_Api>("drycleanerapi")
    .WithReference(mysql, "drycleanerdb")
    .WithReference(generator)
    .WaitFor(mysql)
    .WaitFor(generator)
    .WithEnvironment("OrderGeneration__Enabled", "true");

if (!isTesting)
{
    api.WithEndpoint("https", endpoint => endpoint.Port = 51203)
        .WithEndpoint("http", endpoint => endpoint.Port = 51204);
}
else
{
    api.WithHttpHealthCheck("/health", endpointName: "https");
}

builder.Build().Run();