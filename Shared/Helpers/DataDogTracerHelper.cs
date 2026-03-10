using System.Text.Json;
using Datadog.Trace;
using Microsoft.AspNetCore.Http;

namespace Shared.Helpers;

public static class DataDogTracerHelper
{
    private static string Normalize(string key)
    {
        return key.Replace('-', '_').Replace(':', '_').Replace('.', '_');
    }

    public static void CaptureHttpRequestBody<TBody>(TBody? body)
    {
        var scope = Tracer.Instance.ActiveScope;
        if (scope == null || body == null) return;

        var jsonBody = JsonSerializer.Serialize(body);
        
        if (!string.IsNullOrWhiteSpace(jsonBody))
            scope.Span.SetTag("debug.http.request.raw-body", jsonBody);
    }
    
    public static void CaptureHttpRequestHeaders(HttpRequest request)
    {
        var scope = Tracer.Instance.ActiveScope;
        if (scope == null) return;

        foreach (var headers in request.Headers)
        {
            string key = Normalize(headers.Key);
            string value = Normalize(headers.Value.ToString());
            scope.Span.SetTag(key, value);
        }
    }
    
    public static void CaptureHttpResponse<TBody>(TBody? body)
    {
        var scope = Tracer.Instance.ActiveScope;
        if (scope == null || body == null) return;

        var jsonBody = JsonSerializer.Serialize(body);
        
        var flattenedBody = JsonFlattener.Flatten(jsonBody);
        
        foreach (var pair in flattenedBody)
        {
            scope.Span.SetTag(pair.Key, pair.Value);
        }
     
    }
    
    public static void CaptureHttpResponseHeaders(HttpResponse response)
    {
        var scope = Tracer.Instance.ActiveScope;
        if (scope == null) return;

        foreach (var headers in response.Headers)
        {
            string key = Normalize(headers.Key);
            string value = Normalize(headers.Value.ToString());
            scope.Span.SetTag(key, value);
        }
    }

}