using Microsoft.AspNetCore.Builder;
using Yagang.Extensions.BusinessException.Middleware;

namespace Yagang.Extensions.BusinessException.Builder;

public static class BusinessExceptionAppBuilderExtensions
{
    public static IApplicationBuilder UseBusinessException(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BusinessExceptionMiddleware>();
    }
}
