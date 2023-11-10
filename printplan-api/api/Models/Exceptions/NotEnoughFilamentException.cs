namespace printplan_api.Models.DTO.Exceptions;

public class NotEnoughFilamentException : Exception
{
    public NotEnoughFilamentException()
    {
        
    }

    public NotEnoughFilamentException(string message) : base(message){}
}