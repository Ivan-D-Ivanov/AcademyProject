using System.Text;
using AcademyProject.CommandHandlers;
using AcademyProject.Extensions;
using AcademyProject.HealthChecks;
using AcademyProject.Middleware;
using AcademyProjectDL.Repositories.Mongo;
using AcademyProjectDL.Repositories.MsSQL;
using AcademyProjectModels.ConfigurationSettings;
using AcademyProjectModels.CongigurationSettings;
using AcademyProjectModels.Users;
using AcademyProjectSL.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);
builder.Services.Configure<MyJsonSettings>(
    builder.Configuration.GetSection(nameof(MyJsonSettings)));
builder.Services.Configure<KafkaPublisherSettings>(
    builder.Configuration.GetSection(nameof(KafkaPublisherSettings)));
builder.Services.Configure<KafkaSubscriberSettings>(
    builder.Configuration.GetSection(nameof(KafkaSubscriberSettings)));
builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection(nameof(MongoDbConfiguration)));
builder.Services.Configure<GenericConsumerSettings>(
    builder.Configuration.GetSection(nameof(GenericConsumerSettings)));
builder.Services.Configure<DeliveryConsumerSettings>(
    builder.Configuration.GetSection(nameof(DeliveryConsumerSettings)));

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

//builder.Services.AddHostedService<MyBackgroundService>();
//builder.Services.AddHostedService<KafkaGenericConsumer<int, Book>>();
builder.Services.AddHostedService<PurchaseDataflowService>();
builder.Services.AddHostedService<DeliveryDataFlowService>();

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
