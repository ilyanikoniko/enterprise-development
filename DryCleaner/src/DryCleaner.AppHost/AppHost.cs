var builder = DistributedApplication.CreateBuilder(args);

const string databaseName = "drycleanerdb";

var password = builder.AddParameter("MySqlPassword", secret: true);

var mysql = builder.AddMySql("mysql", password: password)
    .WithDataVolume() 
    .AddDatabase(databaseName);

builder.AddProject<Projects.DryCleaner_Api>("drycleanerapi")
    .WithReference(mysql, "drycleanerdb")
    .WaitFor(mysql);

builder.Build().Run();