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

// Add our Endpoint Routing and Swagger API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ##### Regras de Negócios / Services #####
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

app.UseAuthorization();
app.MapControllers();
app.Run();
