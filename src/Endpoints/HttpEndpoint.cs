using System.Reflection.Metadata;
using Dtos;

namespace Endpoints;

public static class HttpEndpoint
{
    public static void RegisterHttpEndpoint(this WebApplication app)
    {
        app.MapGet("/request-info", getRequestInfo);
        app.MapGet("/search", getSearchInfo);
        app.MapGet("/api/users", PostUsers);
        
    }

    /// <summary>
    /// 課題１　リクエスト情報を取得して返す
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
    /// 課題２　クエリパラメータを取得する
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

    /// <summary>
    /// 課題３ リクエストボディとレスポンスステータス
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IResult PostUsers(HttpUserRequestDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) )
        {
            return TypedResults.BadRequest(new {error = "不正な値です。"});
        }

        if(dto.Email.Contains("@")) {
            return TypedResults.BadRequest(new {error = "Emailに@が含まれていません。"});
        }
        
        var result = new { message = "User created", name = dto.Name};

        return TypedResults.Created("/api/users", result); // TODO Createdを使う？
    }
}