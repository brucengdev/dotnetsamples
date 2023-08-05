using ValidateOnStart;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<MyConfigOptions>()
    .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
    .ValidateDataAnnotations()
    //ValidateOnStart will run validations on the options right away instead
    //of waiting until the options is used
    .ValidateOnStart();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
