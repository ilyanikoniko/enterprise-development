var builder = DistributedApplication.CreateBuilder(args);

const string databaseName = "drycleanerdb";

var password = builder.AddParameter("MySqlPassword", secret: true);

var mysql = builder.AddMySql("mysql", password: password)
    .WithDataVolume()
    .AddDatabase(databaseName);

var generator = builder
    .AddProject<Projects.DryCleaner_Generator_Grpc_Host>(
        "ordergenerator",
        launchProfileName: "https");

builder.AddProject<Projects.DryCleaner_Api>("drycleanerapi")
    .WithReference(mysql, "drycleanerdb")
    .WithReference(generator)
    .WaitFor(mysql)
    .WaitFor(generator)
    .WithEnvironment("OrderGeneration__Enabled", "true")
    .WithEndpoint("https", endpoint => endpoint.Port = 51203)
    .WithEndpoint("http", endpoint => endpoint.Port = 51204);

builder.Build().Run();