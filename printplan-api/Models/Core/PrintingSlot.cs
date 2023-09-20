using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class PrintingSlot
{
    [Key]
    private int Id { get; set; }
    private int Quantity { get; set; }
    private PrintModel CurrentModel { get; set; }
    private DateTime Date { get; set; }
    
    public PrintingSlot(){}
    
}