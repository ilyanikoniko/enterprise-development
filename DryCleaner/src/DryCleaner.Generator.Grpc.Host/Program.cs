using DryCleaner.Generator;
using DryCleaner.Generator.Grpc.Host.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<OrderGenerator>();

var app = builder.Build();

app.MapGrpcService<OrderStreamingService>();

app.Run();