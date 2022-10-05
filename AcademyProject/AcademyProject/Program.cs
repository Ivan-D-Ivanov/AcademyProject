using AcademyProject.Extensions;
using AcademyProject.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.RegisterRepositoriesPerson()
                .RegisterServicePerson()
                .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("Sql Server")
    .AddUrlGroup(new Uri("https://google.bg"), name: "Google Connection");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapHealthChecks("/health");
app.RegisterHealthChecks();

app.Run();
