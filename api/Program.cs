using api.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Define our Database Context
// Ref. https://www.connectionstrings.com
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDataContext>(options => 
    options.UseSqlite("Data Source=mos_database.db;Cache=shared").UseSnakeCaseNamingConvention()
);

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

// Redirecionar HTTP para HTTPs
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
