using MediatrUnitTesting.Abstractions;
using MediatrUnitTesting.Repository.Database;
using MediatrUnitTesting.Repository.Unity;
using MediatrUnitTesting.Repository.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
Uri baseUrl = new Uri(builder.Configuration.GetSection("BaseUrl").GetSection("MediatrUnitTesting").Value);
// Add url for development project.
builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = baseUrl });

// Adding postgreSQL connection
builder.Services.AddDbContext<UnitTestingDbContext>(opt => opt.UseNpgsql(
	builder.Configuration.GetConnectionString("UnitTestingDbContext")));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<UnitTestingDbContext>();
	// use context
	dbContext.Database.EnsureCreated();
}
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
