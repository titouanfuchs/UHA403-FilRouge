using Microsoft.EntityFrameworkCore;
using printplan_api.Models.Core;

namespace printplan_api.Contexts;

public class PrintPlanContext : DbContext
{
    public PrintPlanContext(DbContextOptions<PrintPlanContext> options) : base(options){}

    public DbSet<Printer> Printers;
    public DbSet<PrintModel> PrintModels;
    public DbSet<PrintingSlot> PrintingSlots;
    public DbSet<FilamentSpool> FilamentSpools;
    
}