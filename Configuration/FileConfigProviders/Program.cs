using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddIniFile("MyIniConfig.ini", optional: true, reloadOnChange: true)
    .AddIniFile($"MyIniConfig.{builder.Environment.EnvironmentName}.ini", optional: true, reloadOnChange: true);

//You can also add Json files or XML files
builder.Configuration.AddJsonFile("MyConfig.json",
        optional: true,
        reloadOnChange: true);

builder.Configuration
    .AddXmlFile("MyXMLFile.xml", optional: true, reloadOnChange: true)
    .AddXmlFile($"MyXMLFile.{builder.Environment.EnvironmentName}.xml",
                optional: true, reloadOnChange: true);

/**
 * Adding the config files will make the values in the config files override 
 * values in the default configuration providers (appsettings.json, cli configs, etc)
 * Which is something you normally don't want
 **/

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) =>
{
    var strBuilder = new StringBuilder();
    strBuilder.Append("Configs from INI files\n");
    strBuilder.Append($"MyIniConfig1={config["MyIniConfig1"]}\nMyIniConfig2={config["MyIniConfig2"]}");
    strBuilder.Append($"\nPosition:Title={config["Position:Title"]}\nLogging:LogLevel:Microsoft={config["Logging:LogLevel:Microsoft"]}");

    strBuilder.Append("\n\nConfigs from json file\n");
    strBuilder.Append($"Student:Name={config["Student:Name"]}\nStudent:Age={config["Student:Age"]}");

    strBuilder.Append("\n\nConfigs from XML file\n");
    strBuilder.Append($"MyKey={config["MyKey"]}\nTeacher:Title={config["Teacher:Title"]}\nTeacher:Name={config["Teacher:Name"]}");

    var responseStr = strBuilder.ToString();
    await context.Response.WriteAsync(responseStr);
});

app.Run();
