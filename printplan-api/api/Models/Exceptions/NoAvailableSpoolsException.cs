namespace printplan_api.Models.DTO.Exceptions;

public class NoAvailableSpoolsException : Exception
{
    public NoAvailableSpoolsException(){}

    public NoAvailableSpoolsException(string message) : base(message){}
}