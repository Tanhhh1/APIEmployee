using API.Register;
using API_Blog.Register;
using Application;
using Infrastructure;
var builder = WebApplication.CreateBuilder(args);

builder.Logging.RegisterLoggerServices(builder.Configuration);
builder.Services.RegisterGeneralServices(builder.Configuration);
builder.Services.AddApplicationConfiguration(builder.Configuration);
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.RegisterSwaggerServices();

var app = builder.Build();
app.RegisterGeneralApp(app.Environment);
app.RegisterSwaggerApp(builder);

await app.RegisterSeedDatabase();

app.Run();
