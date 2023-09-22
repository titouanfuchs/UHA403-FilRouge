using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class PrintingSlot
{
    [Key]
    public int Id { get; set; }
    public int Quantity { get; set; }
    public PrintModel CurrentModel { get; set; }
    public DateTime Date { get; set; }
    
    public PrintingSlot(){}
    
}