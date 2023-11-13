using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class FilamentSpool
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }
    public string Color { get; set; }
    public int Lenght { get; set; }
    public int Quantity { get; set; }
}