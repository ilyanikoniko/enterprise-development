using DryCleaner.Generator;
using DryCleaner.Generator.Grpc.Host.Services;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<OrderGenerator>();

var app = builder.Build();

app.MapGrpcService<OrderStreamingService>();

app.Run();