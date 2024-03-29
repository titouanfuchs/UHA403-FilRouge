using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using printplan_api.Contexts;
using printplan_api.Models.Core;
using printplan_api.Models.DTO;
using printplan_api.Models.DTO.Exceptions;
using printplan_api.Services;

namespace api_test;

[TestFixture]
public class PlanTests
{
    private Mock<PrintPlanContext> _mockContext;
    private Mock<ILogger<PlanService>> _mockLogger;

    [SetUp]
    public void Init()
    {
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
        
        _mockContext = new Mock<PrintPlanContext>();
        _mockContext.Setup(c => c.Printers).Returns(printersSet.Object);
        _mockContext.Setup(c => c.PrintModels).Returns(printModelsSet.Object);
        _mockContext.Setup(c => c.PrintingSlots).Returns(printingSlotsSet.Object);

        _mockLogger = new Mock<ILogger<PlanService>>();
    }

    private PlanService MockPlanService()
    {
        return new PlanService(_mockContext.Object, _mockLogger.Object);
    }
    
    /// <summary>
    ///  L'utilisateur veut imprimer X modèle Y fois et possède la quantité de fil d'impression nécéssaire pour effectuer l'impression d'une seule traite
    /// </summary>
    [Test]
    public void Hyp1_RequestIsPrintable()
    {
        var spools = new List<FilamentSpool>
        {
            new FilamentSpool()
            {
                Name = "Filament classique",
                Lenght = 1000,
                Color = "Black",
                Id = 1,
                Quantity = 2
            }
        }.AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        PrintPlanDto result = service.Plan(postQuery);
        
        Assert.That(result.SpoolReplacementEvents.Count, Is.EqualTo(1).Within(0));
    }

    /// <summary>
    /// L'utilisateur veut imprimer X modèle Y fois et possède la quantité de fil d'impression nécéssaire, mais dois remplacer la bobine une ou plusieurs fois
    /// </summary>
    [Test]
    public void Hyp2_ReplaceSpoolDuringPrinting()
    {
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
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };
        
        PrintPlanDto result = service.Plan(postQuery);
        
        Assert.That(result.SpoolReplacementEvents.Count, Is.EqualTo(2).Within(0));
    }

    /// <summary>
    /// L'utilisateur veut imprimer X modèle Y fois et ne possède pas assez de fil d'impression pour effectuer la quantité voulue
    /// </summary>
    [Test]
    public void Hyp3_NotEnoughFilamentToFullyPrint()
    {
        var spools = new List<FilamentSpool>
        {
            new FilamentSpool()
            {
                Name = "Filament classique court",
                Lenght = 1500,
                Color = "Black",
                Id = 1,
                Quantity = 1
            }
        }.AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 4
        };
        
        PrintPlanDto result = service.Plan(postQuery);
        
        Assert.Less(result.PrintQuantity, result.InitialPrintQuantity);
    }

    /// <summary>
    ///  L'utilisateur veut imprimer X modèle Y fois et ne possède pas assez de fil d'impression et il y a un ou plusieurs changement de bobine en cours d'impression
    /// </summary>
    [Test]
    public void Hyp4_NotEnoughFilamentToFullyPrintAndNeedToChangeSpool()
    {
        var spools = new List<FilamentSpool>
        {
            new FilamentSpool()
            {
                Name = "Filament classique court",
                Lenght = 500,
                Color = "Black",
                Id = 1,
                Quantity = 1
            },
            new FilamentSpool()
            {
                Name = "Filament classique court",
                Lenght = 500,
                Color = "Black",
                Id = 1,
                Quantity = 1
            },
            new FilamentSpool()
            {
                Name = "Filament classique court",
                Lenght = 500,
                Color = "Black",
                Id = 1,
                Quantity = 1
            },
        }.AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 4
        };
        
        PrintPlanDto result = service.Plan(postQuery);
        
        if (result.PrintQuantity < result.InitialPrintQuantity && result.SpoolReplacementEvents.Count > 1) Assert.Pass();
        
        Assert.Fail();
    }

    /// <summary>
    /// L'utilisateur veut lancer une impression sans avoir d'imprimantes de renseignée en base de données
    /// </summary>
    [Test]
    public void Hyp5_NoPrinter()
    {
        var spools = new List<FilamentSpool>
        {
            new FilamentSpool()
            {
                Name = "Filament classique court",
                Lenght = 500,
                Color = "Black",
                Id = 1,
                Quantity = 1
            }
        }.AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        
        var printers = new List<Printer>().AsQueryable();

        var printersSet = new Mock<DbSet<Printer>>();
        printersSet.As<IQueryable<Printer>>().Setup(m => m.Provider).Returns(printers.Provider);
        printersSet.As<IQueryable<Printer>>().Setup(m => m.Expression).Returns(printers.Expression);
        printersSet.As<IQueryable<Printer>>().Setup(m => m.ElementType).Returns(printers.ElementType);
        printersSet.As<IQueryable<Printer>>().Setup(m => m.GetEnumerator()).Returns(() => printers.GetEnumerator());
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        _mockContext.Setup(c => c.Printers).Returns(printersSet.Object);
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        try
        {
            service.Plan(postQuery);
        }
        catch (PrinterNotFoundException printerNotFoundException)
        {
            Assert.Pass();
        }
        
        Assert.Fail();
    }
    
    //L'utilisateur veut imprimer sans avoir de bobines de fil en stock
    [Test]
    public void Hyp6_NoSpool()
    {
        var spools = new List<FilamentSpool>().AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        try
        {
            service.Plan(postQuery);
        }
        catch (NoAvailableSpoolsException noAvailableSpoolsException)
        {
            Assert.Pass();
        }
        
        Assert.Fail();
    }
    
    /// <summary>
    /// L'utilisateur tente d'imprimer un modèle qui n'éxiste pas
    /// </summary>
    [Test]
    public void Hyp7_NoModel()
    {
        var printModels = new List<PrintModel>().AsQueryable();
        
        var printModelsSet = new Mock<DbSet<PrintModel>>();
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.Provider).Returns(printModels.Provider);
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.Expression).Returns(printModels.Expression);
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.ElementType).Returns(printModels.ElementType);
        printModelsSet.As<IQueryable<PrintModel>>().Setup(m => m.GetEnumerator()).Returns(() => printModels.GetEnumerator());
        
        var spools = new List<FilamentSpool>().AsQueryable();

        var spoolsSet = new Mock<DbSet<FilamentSpool>>();
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Provider).Returns(spools.Provider);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.Expression).Returns(spools.Expression);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.ElementType).Returns(spools.ElementType);
        spoolsSet.As<IQueryable<FilamentSpool>>().Setup(m => m.GetEnumerator()).Returns(() => spools.GetEnumerator());
        
        _mockContext.Setup(c => c.FilamentSpools).Returns(spoolsSet.Object);
        _mockContext.Setup(c => c.PrintModels).Returns(printModelsSet.Object);
        PlanService service = MockPlanService();

        PostPrintPlanDto postQuery = new PostPrintPlanDto()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        try
        {
            service.Plan(postQuery);
        }
        catch (PrintModelNotFoundException notFoundException)
        {
            Assert.Pass();
        }
        
        Assert.Fail();
    }
}