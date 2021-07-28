# WPF アプリを作ってみよう

### 雛形を作る

次の3つのコマンドを実行します。  
```
dotnet new sln -n AspApp
dotnet new mvc -n AspApp
dotnet sln add ./AspApp\AspApp.csproj
```
これで雛形が作られます。

### Visual Studio Code で開く

雛形のソースコードファイルを Visual Studio Code で開きます。  
次のコマンドを実行します。
```
code .
```
これでソースコードファイル群のディレクトリが Visual Studio Code で開かれます。

### デバッグの設定をする

Visual Studio Code のメニューで次のメニューを選択します。
```
実行 > 構成の追加 > .NET Core
```

初回の選択時に **launch.json** ファイルがが作られ、Visual Studio Code 上で開かれます。

### デバッグ実行をする

Visual Studio Code のメニューで次のメニューを選択します。
```
実行 > デバッグの開始
```
これでプログラムが実行できます。  
しかし今の段階では特にゲームの要素は何もない画面が開くだけです。ここから画面に要素を配置して、入力などを行えるようにして行きます。

### デバッグを終了する

デバッグ実行を行うとブラウザが起動して画面が開きます。実はこのブラウザを閉じてもデバッグは終了しません。  
デバッグを終了するには Visual Studio Code のメニューで次のメニューを選択します。
```
実行 > デバッグの停止
```
これでプログラムが終了できます。  

### 雛形ソースコードのファイルを開く

Visual Studio Code のメニューで次のメニューを選択します。
```
表示 > エクスプローラー
```
Visual Studio Code の左側にエクスプローラー ペインが開きます。その中から **AspApp** をクリックします。現れるいくつかの選択肢の中から **Views**、**Home**、**Index.chtml** と順にクリックします。すると **Index.chtml** というファイルが Visual Studio Code のメインの領域で開きます。この **Index.chtml** ファイルが画面の要素や配置が書かれるファイルです。  
まずは、このファイルを編集して見た目を作ります。

### 画面の見た目を作る

開いたファイルを次のように要素を追加します。

**追加する内容**
```html
<div>
    <form method="post">
        数字あて<br/>
        数字を入力してください<br/>
        <input type="number" name="text" value="@ViewData["Text"]"/><br/>
        <button type="submit">回答</button><br/>
        <input type="hidden" name="answer" value="@ViewData["Answer"]" />
    </form>
    <pre>@ViewData["Message"]</pre>
</div>
```

**変更前**
```html
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>
```

**変更後**
```html
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>

<div>
    <form method="post">
        数字あて<br/>
        数字を入力してください<br/>
        <input type="number" name="text" value="@ViewData["Text"]"/><br/>
        <button type="submit">回答</button><br/>
        <input type="hidden" name="answer" value="@ViewData["Answer"]" />
    </form>
    <pre>@ViewData["Message"]</pre>
</div>
```

#### 解説

今書いていたものは基本は **HTML** というもので **C#** とは別のものです。**HTML** はブラウザの画面を作るためのものです。今回作るアプリはブラウザで使う Web アプリなので、画面は **HTML** で作ります。**C#** は画面を操作した際の動作を作るのに使います。

- ```<br/>```  
これが書かれた場所で画面に表示されるものが改行されます。
- ```<input type="number" name="text" value="@ViewData["Text"]"/>```  
画面に文字の入力枠を表示します。ユーザーによる入力が可能です。プログラムで文字を入力することもできます。
- ```<button type="submit">回答</button>```  
画面にボタンを表示します。マウスでクリックするなどが行えるボタンです。  
- ```<input type="hidden" name="answer" value="@ViewData["Answer"]" />```  
画面に隠し要素を置きます。今回のゲームではこの要素に正解の答えを隠します。  
- ```@ViewData["Text"]```、```@ViewData["Answer"]```、```@ViewData["Message"]```  
これらは **HTML** ではなく **ASP[]().NET Core** の機能で、画面に文字を表示します。表示するのみで、ユーザーによる入力はできません。 

### 雛形ソースコードのファイルを開く

画面の動作を書くプログラムコードを **HomeController.cs** というファイルに書いていきます。  

Visual Studio Code の左側のエクスプローラー ペインで  **Controllers** をクリックし **HomeController.cs** を選択してください。ファイルが開き、次のようになっています。
```cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspApp.Models;

namespace AspApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
```

このファイルに画面の動作を書いて行きます。

<hr />

[< 前へ](./textbook.md) | [次へ >](./textbook02.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)