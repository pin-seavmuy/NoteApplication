using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

// 👇 Add your custom repository interface and class
// (make sure you have the right namespace or place these in the same folder)
using YourNamespace.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add controllers (so you can use NotesController.cs)
builder.Services.AddControllers();

// ✅ Register your NoteRepository as a service
builder.Services.AddScoped<INoteRepository, NoteRepository>();

// ✅ Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notes API", Version = "v1" });
});

// ✅ Build the app
var app = builder.Build();

// ✅ Use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Optional: Use HTTPS redirection
app.UseHttpsRedirection();

// ✅ Map controllers
app.MapControllers();

// ✅ Run the app
app.Run();
