using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Yagang.Extensions.BusinessException.Infrastructure;

namespace Yagang.Extensions.BusinessException.Middleware;

internal sealed class BusinessExceptionMiddleware(
    RequestDelegate next,
    IOptions<BusinessExceptionOptions> options)
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BusinessException ex)
        {
            await HandleExceptionAsync(context, ex, options.Value);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        BusinessException ex,
        BusinessExceptionOptions options)
    {
        context.Response.StatusCode = ex.StatusCode ?? options.StatusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var response = new { ex.Message, context.Response.StatusCode };
        await JsonSerializer.SerializeAsync(context.Response.Body, response, _jsonOptions);
    }
}