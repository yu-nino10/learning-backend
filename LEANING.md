## ここには課題の中でわからなかったことや気づいたことをメモしていく

## HTTPリクエストレスポンス
### 課題１
・Endpointの実装の始め方（引数this WebApplication appやHttpContext contextの意味）
 →WebApplication app：　拡張メソッド。app.RegisterHttpEndpoint()で呼べるようになる
 →HttpContext context：　リクエスト/レスポンスの全情報が入っている

・複数の値をオブジェクト形式でリターンするには？（クラスを作るか、以下の匿名型のオブジェクトでいいか）
　→簡単な結果なら匿名でOK
・リクエストの値を取得する方法（context.Request.Methodなど）
・オブジェクトの{}の中の書き方
    {
        a = a, なのか　→　こっち
        b: b, なのか
    }
・匿名型のオブジェクト
　　var result = new 
    {
        a = a,
        b = b
    }

### 課題２
・クエリパラメータの書き方は単純に引数に書くだけでいいのか？パスパラメータとの違いなど。
　→単純に引数に書くのでOK。パスパラメータの場合も受け取り側は同じ実装でOK

### 課題３
・TypedResults.（BadRequestやCreated）の使い方。引数には何を入れる？
　→TypedResults.Ok(data);           // 200 + データ
　→TypedResults.BadRequest(error);  // 400 + エラーメッセージ
　→TypedResults.Created(uri, data); // 201 + 場所 + データ
　→TypedResults.Unauthorized();     // 401（引数なし）
　→TypedResults.StatusCode(403);    // 任意のコード
・通常は戻り値もDtoにするべき？
　→複雑な戻り値や今後増える可能性がある場合はDto

### 課題４
・HTTPヘッダーから値を取る方法、ヘッダーに値をセットする方法
　例：
　→context.Request.Headers["User-Agent"]
　→context.Response.Headers.Append("X-Custom-Header", upperName);

### 課題５
・ステータスコードの返却（Unauthorizedとかでいいのか、403って書くのかなど）
　→課題３の回答