using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlite("Data Source=mos_database.db;Cache=shared").UseSnakeCaseNamingConvention()
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API - Ficha de RPG", Version = "v1" });
    c.EnableAnnotations();
});

WebApplication app = builder.Build();
ApplyMigrations(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
SetupCORS(app);

// Adicionando o middleware de tratamento de exce��o
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDataContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Um erro ocorreu ao migrar ou criar o banco de dados: {ex.Message}");
    }
}

void SetupCORS(WebApplication app)
{
    app.UseCors(request => request.AllowAnyOrigin());  // Mantenha isso em mente ao migrar para produ��o
}

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Microsoft.Data.Sqlite.SqliteException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            status = context.Response.StatusCode,
            message = exception.Message
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

}
