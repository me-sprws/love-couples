using CouplesService.Application;
using CouplesService.Infrastructure;
using CouplesService.Infrastructure.Persistence.Extensions;
using CouplesService.WebAPI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi(builder.Configuration, builder);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    MigrationsExtensions.ApplyMigrations(app.Services);
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();