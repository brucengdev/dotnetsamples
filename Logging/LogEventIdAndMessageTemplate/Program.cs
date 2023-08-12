var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var id = 1234;
var msg = "Hello!";
var timestamp = DateTime.Now;

//Event ID can be used to group log messages
//The parameters and their values are stored in the log provider.
//The actual values are stored, for example with a SQL provider, there can be 3 columns
//Id, Msg and Time to store the actual value
//So that querying for logs is easier because there is no need to parse through all log
//messages
app.Logger.LogInformation(1234, "This is message for {Id} and the msg is {Msg}, occurred at {Time}", id, msg, timestamp);

app.Run();
