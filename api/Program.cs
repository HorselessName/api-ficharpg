using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    options =>
    {
        // Correção: No routes matched the supplied values.
        options.SuppressAsyncSuffixInActionNames = false;
    }
    );
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
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
else
{
    _ = app.UseHttpsRedirection();
}

app.UseAuthorization();
SetupCORS(app);

// Adicionando o middleware de tratamento de exceção
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

void ApplyMigrations(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;
    try
    {
        AppDataContext context = services.GetRequiredService<AppDataContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Um erro ocorreu ao migrar ou criar o banco de dados: {ex.Message}");
    }
}

void SetupCORS(WebApplication app)
{
    _ = app.UseCors(request => request.AllowAnyOrigin());  // Mantenha isso em mente ao migrar para produção
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
