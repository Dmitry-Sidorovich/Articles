namespace Articles.Contracts.Errors;

public class ErrorDto
{
    public int StatusCodes {get; set;}
    public string Message  {get; set;}
    public string TraceId  {get; set;}
}