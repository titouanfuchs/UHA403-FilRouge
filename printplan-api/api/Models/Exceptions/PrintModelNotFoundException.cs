namespace printplan_api.Models.DTO.Exceptions;

public class PrintModelNotFoundException : Exception
{
    public PrintModelNotFoundException(){}

    public PrintModelNotFoundException(string message) : base(message)
    {
        
    }
}