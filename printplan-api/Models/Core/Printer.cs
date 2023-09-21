using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class Printer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public float PrinterSpeed { get; set; }
    public float PreheatingDuration { get; set; }

    public Printer(){}
}