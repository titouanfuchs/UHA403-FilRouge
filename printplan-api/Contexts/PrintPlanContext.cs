using Microsoft.EntityFrameworkCore;
using printplan_api.Models.Core;

namespace printplan_api.Contexts;

public class PrintPlanContext : DbContext
{
    public PrintPlanContext(DbContextOptions<PrintPlanContext> options) : base(options){}

    public DbSet<Printer> Printers { get; set; }
    public DbSet<PrintModel> PrintModels{ get; set; }
    public DbSet<PrintingSlot> PrintingSlots{ get; set; }
    public DbSet<FilamentSpool> FilamentSpools{ get; set; }
    
}