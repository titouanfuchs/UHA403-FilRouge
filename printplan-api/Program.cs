using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using printplan_api.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PrintPlanContext>(options =>
{
    options.UseNpgsql($"Server=printplan-bdd;Port=5432;Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};Database={Environment.GetEnvironmentVariable("DATABASE")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PrintPlan API",
        Description = "Gestion d'impression 3D à la chaine",
        Contact = new OpenApiContact
        {
            Name = "Titouan Fuchs",
            Url = new Uri("https://portfolio.titouanfuchs.fr")
        }
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PrintPlanContext>();
    dataContext.Database.Migrate();
}

app.Run();
