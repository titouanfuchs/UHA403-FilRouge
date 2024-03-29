using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using printplan_api.Contexts;
using printplan_api.Models.Core;
using printplan_api.Models.Core.Filament;
using printplan_api.Models.DTO;
using printplan_api.Models.DTO.Exceptions;
using printplan_api.Models.Enums;

namespace printplan_api.Services;

public class PlanService
{
    private readonly PrintPlanContext _context;
    private readonly ILogger<PlanService> _logger;
    public PlanService(PrintPlanContext context, ILogger<PlanService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public List<PrintPlanDto> GetPlans()
    {
        List<PrintPlanDto> plans = new List<PrintPlanDto>();
        List<PrintingSlot> slots = _context.PrintingSlots.Include(s => s.CurrentModel).ToList();
        
        foreach (PrintingSlot slot in slots)
        {
            plans.Add(Plan(new PostPrintPlanDto(slot)));
        }
        
        return plans;
    }

    public PrintPlanDto GetPlan(int id)
    {
        PrintingSlot? slot = _context.PrintingSlots.Include(s => s.CurrentModel).Where(s => s.Id == id).FirstOrDefault();

        if (slot is null) throw new ArgumentException($"Slot with id {id} does not exists");

        return Plan(new PostPrintPlanDto(slot), false);
    }

    public void DeletePlan(int id)
    {
        PrintingSlot? slot = _context.PrintingSlots.Include(s => s.CurrentModel).Where(s => s.Id == id).FirstOrDefault();
        if (slot is null) throw new ArgumentException($"Slot with id {id} does not exists");

        _context.PrintingSlots.Remove(slot);
        _context.SaveChanges();
    }

    public void EditPlan(PatchPrintPlanDto input)
    {
        PrintingSlot? slot = _context.PrintingSlots.Include(s => s.CurrentModel).Where(s => s.Id == input.Id).FirstOrDefault();
        if (slot is null) throw new ArgumentException($"Slot with id {input.Id} does not exists");

        slot.Quantity = input.Quantity ?? slot.Quantity;

        _context.SaveChanges();
    }

    public List<PurePlan> GetPurePlans()
    {
        List<PurePlan> plans = new List<PurePlan>();
        List<PrintingSlot> slots = _context.PrintingSlots.Include(s => s.CurrentModel).ToList();
        
        foreach (PrintingSlot slot in slots)
        {
            plans.Add(new PurePlan()
            {
                Quantity = slot.Quantity,
                PrintModelId = slot.CurrentModel.Id,
                Id = slot.Id
            });
        }
        
        return plans;
    }
    
    public PrintPlanDto Plan(PostPrintPlanDto input, bool save = true)
    {
        PrintModel? currentModel = GetModel(input.PrintModelId);
        Printer? currentPrinter = GetPrinter(input.PrinterId);
        List<FilamentSpool> currentSpools = GetSpools();

        if (currentModel is null)
            throw new PrintModelNotFoundException($"Le model a imprimer avec l'Id {input.PrintModelId} n'éxiste pas.");

        if (currentPrinter is null)
            throw new PrinterNotFoundException($"L'imprimante avec l'identifiant {input.PrinterId} n'éxiste pas");

        if (!currentSpools.Any()) 
            throw new NoAvailableSpoolsException("Il n'y a pas de fil disponible");

        FilamentEvaluation eval = new FilamentEvaluation()
        {
            Required = currentModel.RequiredFilamentLenght * input.Quantity,
            Available = AvailableFilamentLenght(currentSpools)
        };

        FilamentStatus filamentStatus = GetFilamentStatus(eval);

        TimeSpan printDuration;
        int printableQty = input.Quantity;

        List<FilamentReplacementEvent> replacementEvents;
        
        switch (filamentStatus)
        {
            case FilamentStatus.Ok:
                _logger.LogInformation("Filament quantity is Fine");
                break;
            
            default:
                _logger.LogWarning("Not Enough Filament");
                float share = eval.Available / eval.Required;
                float exactPrintableQty = (int)MathF.Floor(input.Quantity * share);

                if (exactPrintableQty < 1)
                    throw new NotEnoughFilamentException(
                        $"Il n'y a pas assez de fil disponible pour imprimer {input.Quantity} {currentModel.Name}");

                printableQty = (int)MathF.Floor(input.Quantity * share);
                eval.Required = currentModel.RequiredFilamentLenght * printableQty;
                break;
        }
        
        printDuration = CalcPrintingDuration(eval.Required, currentPrinter.PrinterSpeed, currentPrinter.PreheatingDuration);
        replacementEvents = CalcSpoolsReplacements(currentPrinter.PrinterSpeed, currentPrinter.PreheatingDuration,eval.Required, currentSpools);

        EntityEntry<PrintingSlot>? saved = null;
        if (save)
        {
            PrintingSlot printingSlot = new PrintingSlot()
            {
                CurrentModel = currentModel,
                Quantity = input.Quantity
            };

            saved = _context.PrintingSlots.Add(printingSlot);
            _context.SaveChanges();
        }

        return new()
        {
            Id = saved is not null? saved.Entity.Id : -1,
            Printer = currentPrinter.Id,
            Model = currentModel.Id,
            SpoolReplacementEvents = replacementEvents,
            InitialPrintQuantity = input.Quantity,
            PrintQuantity = printableQty,
            TotalDuration = printDuration,
            UnitDuration = printDuration / printableQty,
            RequiredSpoolQuantity = replacementEvents.Count,
            RequiredFilamentLenght = eval.Required
        };
    }

    private PrintModel? GetModel(int modelId)
    {
        return _context.PrintModels.FirstOrDefault(m => m.Id == modelId);

    }

    private Printer? GetPrinter(int printerId)
    {
        return _context.Printers.FirstOrDefault(p => p.Id == printerId);
    }

    private List<FilamentSpool> GetSpools()
    {
        return _context.FilamentSpools.ToList().OrderBy(s => s.Id).ToList();
    }

    private float AvailableFilamentLenght(List<FilamentSpool> spools)
    {
        float total = 0f;

        foreach (FilamentSpool spool in spools)
        {
            total += spool.Lenght * spool.Quantity;
        }

        return total;
    }

    private FilamentStatus GetFilamentStatus(FilamentEvaluation eval) => eval switch
    {
        { Required: var required, Available: var available } when required <= available => FilamentStatus.Ok,
        _ => FilamentStatus.NotEnough
    };

    private TimeSpan CalcPrintingDuration(float filamentLenght, float printerSpeed, float preheatingDuration)
    {
        float duration = preheatingDuration;
        duration += (filamentLenght / printerSpeed);

        
        return TimeSpan.FromSeconds(duration);
    }
    
    private List<FilamentReplacementEvent> CalcSpoolsReplacements(float printerSpeed, float preheating,float lenght, List<FilamentSpool> spools)
    {
        float remainingLenght = lenght;

        List<FilamentReplacementEvent> events = new List<FilamentReplacementEvent>();

        var firstSpool = spools.First();

        remainingLenght -= firstSpool.Lenght;
        
        events.Add(new FilamentReplacementEvent()
        {
            ReplacementDate = new TimeSpan(),
            SpoolId = firstSpool.Id
        });

        if (remainingLenght <= 0) return events;
        
        foreach (FilamentSpool spool in spools)
        {
            for (int i = 0; i < spool.Quantity; i++)
            {
                FilamentReplacementEvent lastEvent = events.LastOrDefault(new FilamentReplacementEvent() { ReplacementDate = new TimeSpan() });
            
                float spoolDuration = spool.Lenght / printerSpeed + (events.Count == 1 ? preheating : 0);
                TimeSpan duration = TimeSpan.FromSeconds(Math.Floor(spoolDuration));

                events.Add(new (){ReplacementDate = lastEvent.ReplacementDate.Add(duration), SpoolId = spool.Id});

                remainingLenght -= spool.Lenght;   
                
                if (remainingLenght <= 0) return events;
                if (spool.Lenght >= remainingLenght) return events;
            }
        }

        return events;
    }
}