
var builder = DistributedApplication.CreateBuilder(args);

const string databaseName = "drycleanerdb";

var password = builder.AddParameter("MySqlPassword", secret: true);

var mysql = builder.AddMySql("mysql", password: password)
    .WithDataVolume()
    .AddDatabase(databaseName);

builder.AddProject<Projects.DryCleaner_Api>("drycleanerapi")
    .WithReference(mysql, "drycleanerdb")
    .WaitFor(mysql)
    .WithEndpoint("https", endpoint => endpoint.Port = 51203)
    .WithEndpoint("http", endpoint => endpoint.Port = 51204);

builder.Build().Run();
