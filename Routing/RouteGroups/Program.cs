using RouteGroups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//using MapGroup, you can reuse the same method MapTodosApi
//to configure similar groups of endpoints, without having to duplicate code.

app.MapGroup("/public/todos")
    .MapTodosApi();

//different groups can have different configurations like authorization and authentication
app.MapGroup("/private/todos")
    .MapTodosApi()
    .RequireAuthorization();

app.Run();