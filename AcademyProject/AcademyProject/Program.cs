using AcademyProject.Extensions;
using AcademyProject.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using MediatR;
using AcademyProject.CommandHandlers;
using AcademyProject.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AcademyProjectModels.Users;
using AcademyProjectDL.Repositories.MsSQL;

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
builder.Services.AddSwaggerGen(x =>
{
    var jtwTokenSercurity = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token in the text box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    x.AddSecurityDefinition(jtwTokenSercurity.Reference.Id, jtwTokenSercurity);

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {jtwTokenSercurity, Array.Empty<string>()}
    });
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Challenger", policy =>
    {
        policy.RequireClaim("Challenger");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Health checks
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("Sql Server")
    .AddUrlGroup(new Uri("https://google.bg"), name: "Google Connection");

builder.Services.AddMediatR(typeof(GetAllBooksCommandHandler).Assembly);

builder.Services.AddIdentity<UserInfo, UserRole>()
    .AddUserStore<UserInfoStore>()
    .AddRoleStore<UserRoleStore>();
//app builder below
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

//app.MapHealthChecks("/health");
app.RegisterHealthChecks();

app.Run();
