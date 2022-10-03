using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectSL.Interfaces;
using AcademyProjectSL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPersonInMemoryRepository, PersonInMemoryRepository>();
builder.Services.AddSingleton<IAuthorInMemoryRepo, AuthorInMemoryRepo>();
builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
