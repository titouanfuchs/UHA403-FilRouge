using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class FilamentSpool
{
    [Key]
    private int Id { get; set; }
    private string Name { get; set; }
    private string Color { get; set; }
    private int Lenght { get; set; }
    private int Quantity { get; set; }
    
    public FilamentSpool(){}
}