namespace printplan_api.Models.DTO.Exceptions;

public class PrinterNotFoundException : Exception
{
    public PrinterNotFoundException(){}
    
    public PrinterNotFoundException(string message) : base(message){}
}