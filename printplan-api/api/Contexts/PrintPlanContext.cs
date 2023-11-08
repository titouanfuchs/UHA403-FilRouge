using Microsoft.EntityFrameworkCore;
using printplan_api.Models.Core;

namespace printplan_api.Contexts;

public class PrintPlanContext : DbContext
{
    public PrintPlanContext(DbContextOptions<PrintPlanContext> options) : base(options)
    {
    }

    public DbSet<Printer> Printers { get; set; }
    public DbSet<PrintModel> PrintModels { get; set; }
    public DbSet<PrintingSlot> PrintingSlots { get; set; }
    public DbSet<FilamentSpool> FilamentSpools { get; set; }

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

        Random rnd = new Random();
        
        for (int i = 0; i < 10; i++)
        {
            modelBuilder.Entity<FilamentSpool>().HasData(new FilamentSpool()
            {
                Id = i + 1,
                Name = $"Bobine_{i}",
                Lenght = rnd.Next(100, 1000),
                Color = "Black",
                Quantity = rnd.Next(1, 10)
            });
        }

        #endregion
    }
}