using Microsoft.Extensions.DependencyInjection.Extensions;
using TryAddEnumerable;
var builder = WebApplication.CreateBuilder(args);

//example on registering services using ServiceDescriptor
//This should rarely be used, use the usual TryAdd{scope}<TService, TImplementation>() instead
var serviceDescriptor = new ServiceDescriptor(typeof(ITransientService),
                            _ => new TransientService(), 
                            ServiceLifetime.Transient);
builder.Services.Add(serviceDescriptor);

//another way to use ServiceDescriptor
builder.Services.Add(ServiceDescriptor.Transient<ITransientService, TransientService>());

//Use TryAdd to register a new implementation if no implementation of ANY type has been registered for that interface
//this does not do anything as there has already implementation of ITransientService
builder.Services.TryAddTransient<ITransientService, TransientService>();
builder.Services.TryAddTransient<ITransientService, TransientService2>();//this registers a new instance

//Use TryAddEnumerable will ONLY add a service if an EXACT implementation for that EXACT interface does not exist
//Means you can register multiple different implementation types for the same interface
//This does not register anything
//because an TransientService2 implementing ITransientService already exists
builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<ITransientService, TransientService2>());
//This registers a TransientService3 implementation
builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<ITransientService, TransientService3>());

var app = builder.Build();

//inject the transient services to a middleware
//use IEnumerable to get all instances of registered services
//they appear in the order they were registered
app.MapGet("/", (IEnumerable<ITransientService> services) => 
    string.Join("\n", services.Select(s => s.GetMessage()))
);

app.Run();
