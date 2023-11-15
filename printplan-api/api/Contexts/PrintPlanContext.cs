using Microsoft.EntityFrameworkCore;
using printplan_api.Models.Core;

namespace printplan_api.Contexts;

public class PrintPlanContext : DbContext
{
    public PrintPlanContext(){}
    
    public PrintPlanContext(DbContextOptions<PrintPlanContext> options) : base(options)
    {
    }

    public virtual DbSet<Printer> Printers { get; set; }
    public virtual DbSet<PrintModel> PrintModels { get; set; }
    public virtual DbSet<PrintingSlot> PrintingSlots { get; set; }
    public virtual DbSet<FilamentSpool> FilamentSpools { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Printers

        modelBuilder.Entity<Printer>().HasData(new Printer
        {
            Id = 1,
            Name = "Default Printer",
            PreheatingDuration = 120f,
            PrinterSpeed = 10f
        });
        
        modelBuilder.Entity<PrintModel>().HasData(new PrintModel()
        {
            Id = 1,
            Name = "Petit model",
            RequiredFilamentLenght = 5f
        });
        
        modelBuilder.Entity<PrintModel>().HasData(new PrintModel()
        {
            Id = 2,
            Name = "Moyen model",
            RequiredFilamentLenght = 10f
        });
        
        modelBuilder.Entity<PrintModel>().HasData(new PrintModel()
        {
            Id = 3,
            Name = "Grand model",
            RequiredFilamentLenght = 30f
        });
        
        modelBuilder.Entity<PrintModel>().HasData(new PrintModel()
        {
            Id = 4,
            Name = "Gargantua",
            RequiredFilamentLenght = 1000f
        });

        modelBuilder.Entity<FilamentSpool>().HasData(new FilamentSpool()
        {
            Id = 1,
            Name = $"Bobine_Courte",
            Lenght = 100,
            Color = "Black",
            Quantity = 2
        });
        
        modelBuilder.Entity<FilamentSpool>().HasData(new FilamentSpool()
        {
            Id = 2,
            Name = $"Bobine_Moyenne",
            Lenght = 500,
            Color = "Black",
            Quantity = 2
        });
        
        modelBuilder.Entity<FilamentSpool>().HasData(new FilamentSpool()
        {
            Id = 3,
            Name = $"Bobine_Longue",
            Lenght = 2000,
            Color = "Black",
            Quantity = 2
        });

        #endregion
    }
}