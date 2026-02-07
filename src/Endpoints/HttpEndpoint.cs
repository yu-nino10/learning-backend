using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints;

public static class HttpEndpoint
{
    public static void RegisterHttpEndpoint(this WebApplication app)
    {
        app.MapGet("/request-info", getRequestInfo);
        app.MapGet("/search", getSearchInfo);
        app.MapPost("/api/users", PostUsers);
        app.MapGet("/api/headers/info", GetHeadersInfo);
        app.MapGet("/api/status/custom", GetStatusCustom);
        app.MapPost("/api/response/data", PostResponseData);
        
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
    public static IResult PostUsers([FromBody]HttpUserRequestDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) )
        {
            return TypedResults.BadRequest(new {error = "不正な値です。"});
        }

        if(!dto.Email.Contains("@")) {
            return TypedResults.BadRequest(new {error = "Emailに@が含まれていません。"});
        }
        
        var result = new { message = "User created", name = dto.Name};

        return TypedResults.Created("/api/users", result); // TODO Createdを使う？
    }

    /// <summary>
    /// 課題４ HTTPヘッダーとレスポンスステータス
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IResult GetHeadersInfo(HttpContext context)
    {
        var agent = context.Request.Headers["User-Agent"];
        var accept = context.Request.Headers["Accept"];
        
        var result = new {agent = agent, accept = accept};
        return TypedResults.Ok(result);
    }
    public static IResult GetStatusCustom(string code)
    {
        switch (code)
        {
            case "200":
                return TypedResults.Ok("OK");
            case "400":
                return TypedResults.BadRequest("失敗");
            default:
                return TypedResults.NotFound("見つからない");
        }
    }
    public static IResult PostResponseData([FromBody] string name, HttpContext context)
    {
        var upperName = name.ToUpper();
        context.Response.Headers.Append("X-Custom-Header", upperName);
        return TypedResults.Ok();
    }
}