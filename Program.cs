using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ApiMvc.Models; // Certifique-se de adicionar este using.
using Swashbuckle.AspNetCore.Annotations;
using ApiMvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMySql<ApplicationDbContext>(
    builder.Configuration["Database:MySql"],
    new MySqlServerVersion(new Version(8, 0, 21)),
    mySqlOptions => { });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); // Adicione este middleware antes do CORS

app.UseCors(); // Adicione este middleware para habilitar o CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
