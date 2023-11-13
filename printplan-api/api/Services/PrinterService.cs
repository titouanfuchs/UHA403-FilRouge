using printplan_api.Contexts;
using printplan_api.Models.Core;
using printplan_api.Models.DTO.Printer;

namespace printplan_api.Services;

public class PrinterService
{
    private readonly PrintPlanContext _dataContext;

    public PrinterService()
    {
    }

    public PrinterService(PrintPlanContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<PrinterSettingsDTO> PatchSettings(int printerId, PrinterSettingsDTO inputData)
    {
        var currentPrinter = _dataContext.Printers.FirstOrDefault(p => p.Id == printerId);

        if (currentPrinter is null) throw new NullReferenceException($"Imprimante avec Id {printerId}, n'éxiste pas.");

        if (inputData.PrinterSpeed is not null)
            EditPrintingSpeedSettings(currentPrinter, (float)inputData.PrinterSpeed);
        if (inputData.PreheatingDuration is not null)
            EditPrinterPreheatingDuration(currentPrinter, (float)inputData.PreheatingDuration);

        await _dataContext.SaveChangesAsync();

        return new PrinterSettingsDTO(currentPrinter);
    }

    public static Printer EditPrintingSpeedSettings(Printer currentPrinter, float printerSpeeed)
    {
        if (printerSpeeed <= .0f)
            throw new ArgumentException("La vitesse ne peut pas être inférieure ou égale à 0");

        if (printerSpeeed == currentPrinter.PrinterSpeed)
            throw new Exception("Le paramètre de vitesse est identique à celui enregistré en base de données");

        currentPrinter.PrinterSpeed = printerSpeeed;

        return currentPrinter;
    }

    public static Printer EditPrinterPreheatingDuration(Printer currentPrinter, float preheatingDuration)
    {
        if (preheatingDuration <= .0f)
            throw new ArgumentException("La durée de préchauffage ne peut pas être inférieure ou égale a 0");

        if (preheatingDuration == currentPrinter.PreheatingDuration)
            throw new Exception(
                "Le paramètre de durée de préchauffage est identique à celui enregistré en base de données");

        currentPrinter.PreheatingDuration = preheatingDuration;

        return currentPrinter;
    }
}