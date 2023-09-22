using printplan_api.Models.Core;
using printplan_api.Services;

namespace api_test;

[TestFixture]
public class PrinterTests
{
    /// <summary>
    /// L'utilisateur entre une valeur de vitesse d'impression cohérente (> 0 et différente du paramètre déjà définis)
    /// </summary>
    [Test]
    public void Hyp1ModificationPrinterSpeed()
    {
        Printer currentPrinter = new Printer()
        {
            Id = 0,
            Name = "Butiful pinter",
            PrinterSpeed = 100f,
            PreheatingDuration = 100f
        };

        var result = PrinterService.EditPrintingSpeedSettings(currentPrinter, 10f);
        
        if (result.PrinterSpeed != 10f) Assert.Fail();
        
        Assert.Pass();
    }
    
    /// <summary>
    /// L'utilisateur tente de modifier la vitesse d'impression mais la valeur est égale au paramètre en base de données 
    /// </summary>
    [Test]
    public void Hyp2NoPrinterSpeedModification()
    {
        Printer currentPrinter = new Printer()
        {
            Id = 0,
            Name = "Butiful pinter",
            PrinterSpeed = 100f,
            PreheatingDuration = 100f
        };


        try
        {
            var result = PrinterService.EditPrintingSpeedSettings(currentPrinter, 100f);

            if (result.PrinterSpeed != 100f) Assert.Fail();

            Assert.Pass();
        }
        catch (NullReferenceException ne)
        {
            Assert.Fail();

        }
        catch (ArgumentException ae)
        {
            Assert.Fail();
        }
        catch (Exception e)
        {
            Assert.Pass();
        }

    }
    
    /// <summary>
    /// L'utilisateur tente de définir une vitesse d'impréssion inférieure ou égale à 0
    /// </summary>
    [Test]
    public void Hyp3NullOrNegativeSpeed()
    {
        Printer currentPrinter = new Printer()
        {
            Id = 0,
            Name = "Butiful pinter",
            PrinterSpeed = 100f,
            PreheatingDuration = 100f
        };


        try
        {
            var result = PrinterService.EditPrintingSpeedSettings(currentPrinter, -100f);

            if (result.PrinterSpeed <= 0f) Assert.Fail();

            Assert.Pass();
        }
        catch (NullReferenceException ne)
        {
            Assert.Fail();

        }
        catch (ArgumentException ae)
        {
            Assert.Pass();
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

    }
}