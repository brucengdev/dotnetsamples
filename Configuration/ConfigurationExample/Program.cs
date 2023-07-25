using System.Text;
using ConfigurationExample;

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

    //other methods to get configs from IConfiguration
    var number = config.GetValue<int>("ANumber");
    builder.Append($"\nANumber={number}");
    
    //GetSection, set breakpoints here to check
    var positionSection = config.GetSection("Position");
    var title = positionSection["Title"];
    var name = positionSection["Name"];

    var children = config.GetChildren();
    var barSection = config.GetSection("Bar");
    if (!barSection.Exists())
    {
        builder.Append("\nSection Bar does not exist");
    }
    
    //Binding to arrays
    var arrayExample = config.GetSection("Array").Get<ArrayExample>();
    builder.Append($"\n\nArray example");
    builder.Append($"\nArray.ArrayWithKeys={string.Join(",", arrayExample.ArrayWithKeys)}");
    builder.Append($"\nArray.Array={string.Join(",", arrayExample.Array)}");
    
    return builder.ToString();
});

app.Run();
