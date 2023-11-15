using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using printplan_api.Contexts;
using printplan_api.Models.Core;
using printplan_api.Models.DTO.Spools;

namespace printplan_api.Services;

public class SpoolsService
{
    private readonly PrintPlanContext _context;
    
    public SpoolsService(PrintPlanContext context)
    {
        _context = context;
    }

    public async Task ResetSpools()
    {
        await ClearSpools();
        
        List<FilamentSpool> spools = new List<FilamentSpool>()
        {
            new FilamentSpool()
            {
                Id = 1,
                Name = $"Bobine_Courte",
                Lenght = 100,
                Color = "Black",
                Quantity = 2
            },
            new FilamentSpool()
            {
                Id = 2,
                Name = $"Bobine_Moyenne",
                Lenght = 500,
                Color = "Black",
                Quantity = 2
            },
            new FilamentSpool()
            {
                Id = 3,
                Name = $"Bobine_Longue",
                Lenght = 2000,
                Color = "Black",
                Quantity = 2
            }
        };
        
        spools.ForEach(s => _context.FilamentSpools.Add(s));

        await _context.SaveChangesAsync();
    }

    public async Task<FilamentSpool> CreateSpool(PostSpoolDto input)
    {
        FilamentSpool newSpool = new FilamentSpool()
        {
            Color = "Black",
            Lenght = input.Lenght,
            Name = input.Name,
            Quantity = input.Quantity
        };

        _context.FilamentSpools.Add(newSpool);

        await _context.SaveChangesAsync();

        return newSpool;
    }

    public async Task ClearSpools()
    {
        _context.FilamentSpools.RemoveRange(_context.FilamentSpools);
        await _context.SaveChangesAsync();
    }
}