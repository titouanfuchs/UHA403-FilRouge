using printplan_api.Models.Core;
using printplan_api.Services;

namespace api_test;

[TestFixture]
public class PrinterTests
{
    /// <summary>
    ///     L'utilisateur entre une valeur de vitesse d'impression cohérente (> 0 et différente du paramètre déjà définis)
    /// </summary>
    [Test]
    public void Hyp1ModificationPrinterSpeed()
    {
        var currentPrinter = new Printer
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
    ///     L'utilisateur tente de modifier la vitesse d'impression mais la valeur est égale au paramètre en base de données
    /// </summary>
    [Test]
    public void Hyp2NoPrinterSpeedModification()
    {
        var currentPrinter = new Printer
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
    ///     L'utilisateur tente de définir une vitesse d'impréssion inférieure ou égale à 0
    /// </summary>
    [Test]
    public void Hyp3NullOrNegativeSpeed()
    {
        var currentPrinter = new Printer
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

    /// <summary>
    ///     L'utilisateur entre une valeur de durée de préchauffage cohérente (> 0 et différente du paramètre déjà définis)
    /// </summary>
    [Test]
    public void Hyp1ModificationPreheatingDuration()
    {
        const float newPreheatingDuration = 10f;

        var currentPrinter = new Printer
        {
            Id = 0,
            Name = "Butiful pinter",
            PrinterSpeed = 100f,
            PreheatingDuration = 100f
        };

        var result = PrinterService.EditPrinterPreheatingDuration(currentPrinter, newPreheatingDuration);

        if (result.PreheatingDuration != newPreheatingDuration) Assert.Fail();

        Assert.Pass();
    }

    /// <summary>
    ///     L'utilisateur tente de modifier la durée de préchauffage mais la valeur est égale au paramètre en base de données
    /// </summary>
    [Test]
    public void Hyp2NoPreheatingDurationModification()
    {
        const float preheatingDuration = 100f;

        var currentPrinter = new Printer
        {
            Id = 0,
            Name = "Butiful pinter",
            PrinterSpeed = 100f,
            PreheatingDuration = preheatingDuration
        };

        try
        {
            var result = PrinterService.EditPrinterPreheatingDuration(currentPrinter, preheatingDuration);

            if (result.PreheatingDuration != preheatingDuration) Assert.Fail();

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
    ///     L'utilisateur tente de définir une durée de préchauffage inférieure ou égale à 0
    /// </summary>
    [Test]
    public void Hyp3NullOrNegativePreheatingDuration()
    {
        const float newPreheatingDuration = -100f;

        var currentPrinter = new Printer
        {
            Id = 0,
            Name = "Butiful pinter",
            PrinterSpeed = 100f,
            PreheatingDuration = 100f
        };


        try
        {
            var result = PrinterService.EditPrinterPreheatingDuration(currentPrinter, newPreheatingDuration);

            if (result.PreheatingDuration <= 0f) Assert.Fail();

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