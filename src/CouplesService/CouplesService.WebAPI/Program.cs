using CouplesService.Application;
using CouplesService.Infrastructure;
using CouplesService.Infrastructure.Persistence.Extensions;
using CouplesService.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    MigrationsExtensions.ApplyMigrations(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();