namespace printplan_api.Models.Core;

public class PrintingSlot
{
    private int Id { get; set; }
    private int Quantity { get; set; }
    private object CurrentModel { get; set; }
    private DateTime Date { get; set; }
    
    public PrintingSlot(){}
    
}