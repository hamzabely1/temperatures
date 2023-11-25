using Api.Temeprature.IoC.Application;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Configure Database connexion
builder.Services.ConfigureDBContext(configuration);

//Dependency Injection
builder.Services.ConfigureInjectionDependencyRepository();

builder.Services.ConfigureInjectionDependencyService();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Inculde xml comment on the swagger view
var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

builder.Services.AddSwaggerGen(options => 
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename))
);

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

public partial class Program { }
