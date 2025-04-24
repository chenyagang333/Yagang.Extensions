namespace Yagang.Extensions.BusinessException;

public class BusinessException : Exception
{
    public int? StatusCode { get; }

    public BusinessException(string message) : base(message)
    {
    }
    public BusinessException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
