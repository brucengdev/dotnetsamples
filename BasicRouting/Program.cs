var builder = WebApplication.CreateBuilder(args);

//WebApplicationBuilder calls UseRouting and UseEndpoints already when it configures a pipeline
//So you typically don't have to call it explicitly, unless you want to reorder the middlewares

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello/{name:alpha}", (string name) => $"Hello, {name}!");

//use with authoriazation

//add authorization policy to an endpoint
//This information is called endpoint metadata
app.MapHealthChecks("/healthz").RequireAuthorization();

app.Run();