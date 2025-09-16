using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotesApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add controllers
builder.Services.AddControllers();

// ✅ Register your NoteRepository as a service
builder.Services.AddScoped<INoteRepository, NoteRepository>();

// ✅ Add CORS to allow frontend (localhost:5173) to access API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // 👈 Frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ✅ Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notes API", Version = "v1" });
});

var app = builder.Build();

// ✅ Enable CORS
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();
