using printplan_api.Models.Core;
using printplan_api.Services;

namespace api_test;

[TestFixture]
public class PrinterTests
{
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