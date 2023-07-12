using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//list configuration providers in the order they are used
app.MapGet("/", (HttpContext context, IConfiguration config) => {
    var configRoot = (IConfigurationRoot)config;
    var builder = new StringBuilder();
    
    //append providers
    builder.Append("Providers\n\n");
    builder.Append(string.Join("\n", configRoot.Providers.Select(p => p.ToString()).ToList()));

    //show some configs
    var keys = new string[] {
        "AConfig",
        "ComplexConfig:SubConfig",
        "Position:Title",//from command line args
        "Position:Name",//from command line args
        "DOTNET_STUDENT_FIRST_NAME",
        "DOTNET_STUDENT_LAST_NAME",
        "ASPNETCORE_STUDENT_LAST_NAME"
    };
    builder.Append("\n\nConfigurations:\n\n");
    builder.Append(string.Join("\n", keys.Select(k => $"{k}: {config[k]}")));
    return builder.ToString();
});

app.Run();
