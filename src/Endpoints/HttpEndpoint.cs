public static class HttpEndpoint
{
    public static void RegisterHttpEndpoint(this WebApplication app)
    {
        app.MapGet("/request-info", getRequestInfo);
        app.MapGet("/search", getSearchInfo);
        
    }

    /// <summary>
    /// 課題1　リクエスト情報を取得して返す
    /// </summary>
    /// <param name="context"></param>
    /// <returns>リクエスト情報</returns>
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

    /// <summary>
    /// 課題2　クエリパラメータを取得する
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="limit"></param>
    /// <returns>クエリパラメータ情報</returns>
    public static IResult getSearchInfo(string? keyword, int? limit)
    {
        if(limit == null) limit = 10;

        var result = new
        {
            keyword = keyword,
            limit = limit
        };
        return TypedResults.Ok(result);
    }

}