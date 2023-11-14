using Moq;
using printplan_api.Contexts;
using printplan_api.Models.Core;

namespace api_integration_tests.Helpers;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PrintPlanContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddLogging();
            
            services.AddSingleton(MockDatabase());
        });

        return base.CreateHost(builder);
    }

    private Mock<PrintPlanContext> MockDatabase()
    {
        var mockContext = new Mock<PrintPlanContext>();
        
        var printers = new List<Printer>
        {
            new Printer()
            {
                Id = 1,
                Name = "Test Printer",
                PreheatingDuration = 120f,
                PrinterSpeed = .1f
            }
        }.AsQueryable();

        var printersSet = new Mock<DbSet<Printer>>();
        printersSet.As<IQueryable<Printer>>().Setup(m => m.Provider).Returns(printers.Provider);
        printersSet.As<IQueryable<Printer>>().Setup(m => m.Expression).Returns(printers.Expression);
        printersSet.As<IQueryable<Printer>>().Setup(m => m.ElementType).Returns(printers.ElementType);
        printersSet.As<IQueryable<Printer>>().Setup(m => m.GetEnumerator()).Returns(() => printers.GetEnumerator());
        
        var printModels = new List<PrintModel>()
        {
            new PrintModel()
            {
                Id = 1,
                Name = "Petit model",
                RequiredFilamentLenght = 500f
            },
            new PrintModel()
            {
                Id = 2,
                Name = "Moyen model",
                RequiredFilamentLenght = 1000f
            },
            new PrintModel()
            {
                Id = 3,
                Name = "Grand model",
                RequiredFilamentLenght = 3000f
            },
            new PrintModel()
            {
                Id = 4,
                Name = "Gargantua",
                RequiredFilamentLenght = 10000f
            }
        }.AsQueryable();
        
        var printModelsSet = new Mock<DbSet<PrintModel>>();
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.Provider).Returns(printModels.Provider);
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.Expression).Returns(printModels.Expression);
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.ElementType).Returns(printModels.ElementType);
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.GetEnumerator()).Returns(() => printModels.GetEnumerator());

        var printingSlots = new List<PrintingSlot>().AsQueryable();
        
        var printingSlotsSet = new Mock<DbSet<PrintingSlot>>();
        printingSlotsSet.As<IQueryable<PrintingSlot>>().Setup(m => m.Provider).Returns(printingSlots.Provider);
        printingSlotsSet.As<IQueryable<PrintingSlot>>().Setup(m => m.Expression).Returns(printingSlots.Expression);
        printingSlotsSet.As<IQueryable<PrintingSlot>>().Setup(m => m.ElementType).Returns(printingSlots.ElementType);
        printingSlotsSet.As<IQueryable<PrintingSlot>>().Setup(m => m.GetEnumerator()).Returns(() => printingSlots.GetEnumerator());
        
        var spools = new List<FilamentSpool>
        {
            new FilamentSpool()
            {
                Name = "Filament classique court",
                Lenght = 200,
                Color = "Black",
                Id = 1,
                Quantity = 1
            },
            new FilamentSpool()
            {
                Name = "Filament classique long",
                Lenght = 1000,
                Color = "Black",
                Id = 2,
                Quantity = 1
            }
        }.AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        mockContext = new Mock<PrintPlanContext>();
        mockContext.Setup(c => c.Printers).Returns(printersSet.Object);
        mockContext.Setup(c => c.PrintModels).Returns(printModelsSet.Object);
        mockContext.Setup(c => c.PrintingSlots).Returns(printingSlotsSet.Object);

        return mockContext;
    }
}
