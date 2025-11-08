using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger & OpenAPI
builder.Services.AddEndpointsApiExplorer(); // generuje metadane o endpointach
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pizza Manufacture API",
        Version = "v1",
        Description = "REST API",
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // generuje swagger.json
    app.UseSwaggerUI(c => // dodaje interfejs Swagger UI
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Manufacture API v1");
        c.RoutePrefix = string.Empty; // Swagger dostêpny pod adresem: https://localhost:5001/
    });

    app.MapOpenApi(); // opcjonalne — mo¿e zostaæ, jeœli chcesz u¿ywaæ nowego OpenAPI endpointu
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
