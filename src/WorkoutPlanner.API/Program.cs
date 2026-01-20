using System.Reflection;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise.Interfaces;
using WorkoutPlanner.Domain.Interfaces;
using WorkoutPlanner.Infrastructure.Database.Repositories;
using WorkoutPlanner.Infrastructure.MongoDb;
using WorkoutPlanner.Infrastructure.MongoDb.Settings;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise.Interfaces;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise.Interfaces;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise;
using WorkoutPlanner.Domain.Interfaces.ExternalServices;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI;
using Microsoft.Extensions.Options;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Settings;
using System.Net.Http.Headers;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Esto hace que todos los Guid se serialicen con la representación “standard”
BsonSerializer.RegisterSerializer(
    new GuidSerializer(GuidRepresentation.Standard)
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Incluye el XML de comentarios
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Habilita anotaciones de atributos [SwaggerOperation], [SwaggerTag]
    c.EnableAnnotations();
    // habilita los ejemplos de request/response
    c.ExampleFilters();

    // Configuración de la documentación inicial
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🏋🏻‍♀️ WorkoutPlanner API",
        Version = "v1",
        Description = "API que permite a los usuarios construir planes de entrenamientos flexibles, combinando ejercicios de fuerza y cardio en función de su disponibilidad semanal, con una experiencia enriquecida por contenido multimedia. Sustituye métodos tradicionales manuales (papel, excel) con un sistema automatizado, escalable y personalizado."
    });
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));

builder.Services.AddSingleton<MongoService>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IExerciseOpenAIRepository, ExerciseOpenAIRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<ICreateExerciseUseCase, CreateExerciseUseCase>();
builder.Services.AddScoped<IReadExerciseUseCase, ReadExerciseUseCase>();
builder.Services.AddScoped<IDeleteExerciseUseCase, DeleteExerciseUseCase>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
builder.Services.AddHttpClient<IOpenAIClient, OpenAIService>((prov, client) =>
{
    var opts = prov.GetRequiredService<IOptions<OpenAISettings>>().Value;
    if (string.IsNullOrWhiteSpace(opts.BaseURL))
    {
        throw new InvalidOperationException("OpenAISettings.BaseURL no está configurado en appsettings.json.");
    }
    client.BaseAddress = new Uri(opts.BaseURL);
    client.Timeout = TimeSpan.FromSeconds(
                            opts.TimeoutSeconds > 0
                            ? opts.TimeoutSeconds
                            : 30
                        );
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", opts.ApiKey);
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();
var enableSwagger = builder.Configuration.GetValue<bool>("Swagger:Enabled");

// Configure the HTTP request pipeline.
if (enableSwagger || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
