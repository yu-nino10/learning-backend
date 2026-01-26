public static class HttpEndpoint
{
    public static void RegisterHttpEndpoint(this WebApplication app)
    {
        app.MapGet("/request-info", getRequestInfo);
    }

    public static IResult getRequestInfo(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var host = context.Request.Host;

        var result = new
        {
            method = method,
            path = path,
            host = host
        };
        return TypedResults.Ok(result);
    }

}