using Microsoft.Data.Sqlite;
using printplan_api.Contexts;

namespace api_integration_tests.Helpers;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var dbConn = new SqliteConnection("Filename=:memory:");
        dbConn.Open();
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PrintPlanContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddLogging();
            
            services.AddDbContext<PrintPlanContext>(options =>
            {
                options.UseSqlite(dbConn);
            });
        });

        var app = base.CreateHost(builder);
        
        using (var scope = app.Services.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<PrintPlanContext>();
            dataContext.Database.Migrate();
        }

        return app;
    }
}
