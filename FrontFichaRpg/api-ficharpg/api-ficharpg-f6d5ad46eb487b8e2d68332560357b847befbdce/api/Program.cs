using api.Data;
using Microsoft.EntityFrameworkCore;

// Define our Web API Builder
var builder = WebApplication.CreateBuilder(args);

// Define our Database Context
// Ref. https://www.connectionstrings.com
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDataContext>(options => 
    options.UseSqlite("Data Source=mos_database.db;Cache=shared").UseSnakeCaseNamingConvention()
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add our Endpoint Routing and Swagger API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ##### Regras de Neg�cios / Services #####
// Carregar no banco de dados as habilidades com o HabilidadesService.
builder.Services.AddSingleton<api.Services.HabilidadesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirecionar HTTP para HTTPs
// app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
