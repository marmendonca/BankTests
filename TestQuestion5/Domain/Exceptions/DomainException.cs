namespace TestQuestion5.Domain.Exceptions;

public class DomainException : Exception
{
    public string Type { get; set; }

    public DomainException(string message, string type) : base(message)
    {
        Type = type;
    }
}