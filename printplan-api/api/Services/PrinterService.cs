using Microsoft.EntityFrameworkCore;
using printplan_api.Contexts;
using printplan_api.Models.Core;
using printplan_api.Models.DTO.Printer;

namespace printplan_api.Services;

public class PrinterService
{
    private PrintPlanContext _dataContext;
    
    public PrinterService(PrintPlanContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<PrinterSettingsDTO> PatchSettings(int printerId,PrinterSettingsDTO inputData)
    {
        Printer? currentPrinter = _dataContext.Printers.FirstOrDefault(p => p.Id == printerId);

        if (currentPrinter is null) throw new NullReferenceException($"Imprimante avec Id {printerId}, n'éxiste pas.");

        await EditPrintingSpeedSettings(currentPrinter, inputData.PrinterSpeed);

        await _dataContext.SaveChangesAsync();
        
        return inputData;
    }

    private async Task<Printer> EditPrintingSpeedSettings(Printer currentPrinter, float printerSpeeed)
    {
        if (printerSpeeed <= .0f)
            throw new ArgumentException("La vitesse ne peut pas être inférieure ou égale à 0");

        if (printerSpeeed == currentPrinter.PrinterSpeed)
            throw new Exception("Le paramètre de vitesse est identique à celui enregistré en base de données");

        currentPrinter.PrinterSpeed = printerSpeeed;

        return currentPrinter;
    }
}