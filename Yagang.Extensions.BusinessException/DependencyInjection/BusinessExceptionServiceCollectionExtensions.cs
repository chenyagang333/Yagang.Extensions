using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Yagang.Extensions.BusinessException.Infrastructure;

namespace Yagang.Extensions.BusinessException.DependencyInjection;

public static class BusinessExceptionServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessException(this IServiceCollection services)
        => services.AddBusinessException(_ => { });

    public static IServiceCollection AddBusinessException(this IServiceCollection services, Action<BusinessExceptionOptions> setupAction)
        => services.Configure(setupAction);

    public static IServiceCollection AddBusinessException(this IServiceCollection services, BusinessExceptionOptions businessExceptionOptions)
        => services.Configure<BusinessExceptionOptions>(options =>
        {
            options.StatusCode = businessExceptionOptions.StatusCode;
        });
}
