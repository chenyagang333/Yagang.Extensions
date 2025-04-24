using Microsoft.AspNetCore.Http;

namespace Yagang.Extensions.BusinessException.Infrastructure;

public class BusinessExceptionOptions
{
    public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
}

