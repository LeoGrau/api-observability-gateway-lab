using System.Text;
using System.Text.Json;
using Datadog.Trace;

public static class DataDogTracingHelper
{
  public static async Task CaptureHttpRequest<TRBody>(HttpRequest request, TRBody? requestBody)
  {
    var scope = Tracer.Instance.ActiveScope;
    if (scope == null) return;

    request.EnableBuffering();
    request.Body.Position = 0;

    var jsonBody = JsonSerializer.Serialize(requestBody);
   
    Console.WriteLine($"Here is: {jsonBody}");
    if (!string.IsNullOrEmpty(jsonBody))
    {
      scope.Span.SetTag("http.request.body", jsonBody);
    }

    request.Body.Position = 0;
  }
}