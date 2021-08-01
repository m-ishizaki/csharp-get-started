# 進歩した ASP[]().NET Core アプリを作ってみよう

## 二つのゲームを同時に進行するプログラムコード

ひとまず、**クラス**、**メソッド** を使わずに二つのゲームを進行するプログラムコードが出来上がりました。現在のプログラムコードは次のようになっています。  

**Index.cshtml**
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
        <input type="hidden" name="answer1" value="@ViewData["Answer1"]" />
        <input type="hidden" name="answer2" value="@ViewData["Answer2"]" />
        <input type="hidden" name="cleared1" value="@ViewData["Cleared1"].ToString()" />
        <input type="hidden" name="cleared2" value="@ViewData["Cleared2"].ToString()" />
    </form>
    <pre>@ViewData["Message"]</pre>
</div>
```
**HomeController.cs**
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

        public IActionResult Index(string text, int answer1, int answer2, bool cleared1, bool cleared2)
        {
            if (!(answer1 > 0 && answer1 < 10))
            {
                answer1 = new System.Random().Next(1, 9);
                answer2 = new System.Random().Next(1, 9);
            }
            else if (!int.TryParse(text, out var input))
            {
                ViewData["Message"] = $"{text} → 数字を入力してください";
            }
            else if (input < 1 || input > 9)
            {
                ViewData["Message"] = $"{input} → 1-9 の数字を 1 文字入力してください";
            }
            else
            {
                if (cleared1)
                {
                    ViewData["Message"] += $"{input} → ゲーム1: クリア済みです";
                }
                else
                {
                    if(input == answer1)
                    {
                        ViewData["Message"]  += $"{input} → ゲーム1: 正解！ クリアです";
                        cleared1 = true;
                    }
                    else if (input > answer1)
                    {
                        ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと小さい値です";
                    }
                    else if (input < answer1)
                    {
                        ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと大きい値です";
                    }
                }
                ViewData["Message"] += "\n";
                if (cleared2)
                {
                    ViewData["Message"] += $"{input} → ゲーム2: クリア済みです";
                }
                else
                {
                    if(input == answer2)
                    {
                        ViewData["Message"]  += $"{input} → ゲーム2: 正解！ クリアです";
                        cleared2 = true;
                    }
                    else if (input > answer2)
                    {
                        ViewData["Message"] += $"{input} → ゲーム2: 答えはもっと小さい値です";
                    }
                    else if (input < answer2)
                    {
                        ViewData["Message"] += $"{input} → ゲーム2: 答えはもっと大きい値です";
                    }
                }                
            }
            if (cleared1 && cleared2)
            {
                ViewData["Message"] += "\nゲームクリア！ ";
            }
            ViewData["Text"] = text;
            ViewData["Answer1"] = answer1;
            ViewData["Answer2"] = answer2;
            ViewData["Cleared1"] = cleared1;
            ViewData["Cleared2"] = cleared2;
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

## メソッドを使ってみる

**メソッド** を使ってプログラムコードを改善してみましょう。  
**※このコンテンツは入門者にプログラムコードを書いて動かしてもらうことを目的としているので、メソッド の詳しいことは別途学習してください。**  

**メソッド** を使うとある程度のプログラムの処理をひとまとめにして扱うことができるようになります。わかりやすい例としては、複数回書かれているコードの組を 1 度書くだけで良くなります。例えば今回のコードでは、次の部分が、ゲーム 1 とゲーム 2 用でほぼ同じコードが 2 回書かれています。これを **メソッド** にしてみましょう。  

**変更前**
```cs
if (cleared1)
{
    ViewData["Message"] += $"{input} → ゲーム1: クリア済みです";
}
else
{
    if(input == answer1)
    {
        ViewData["Message"]  += $"{input} → ゲーム1: 正解！ クリアです";
        cleared1 = true;
    }
    else if (input > answer1)
    {
        ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと小さい値です";
    }
    else if (input < answer1)
    {
        ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと大きい値です";
    }
}
ViewData["Message"] += "\n";
if (cleared2)
{
    ViewData["Message"] += $"{input} → ゲーム2: クリア済みです";
}
else
{
    if(input == answer2)
    {
        ViewData["Message"]  += $"{input} → ゲーム2: 正解！ クリアです";
        cleared2 = true;
    }
    else if (input > answer2)
    {
        ViewData["Message"] += $"{input} → ゲーム2: 答えはもっと小さい値です";
    }
    else if (input < answer2)
    {
        ViewData["Message"] += $"{input} → ゲーム2: 答えはもっと大きい値です";
    }
}
```

**メソッド** を追加します。

```cs
(bool cleared, string message) Proceed(int gameNo, bool cleared, int answer, int input)
{
    var no = $"ゲーム{gameNo}: ";
    if (cleared)
    {
        return (true, $"{input} → {no}クリア済みです");
    }
    if (input > answer)
    {
        return (false, $"{input} → {no}答えはもっと小さい値です");
    }
    if (input < answer)
    {
        return (false, $"{input} → {no}答えはもっと大きい値です");
    }
    return (true, $"{input} → {no}正解！ クリアです");
}
```

ゲーム 1、ゲーム 2 の処理をこの **メソッド** を使うように変更します。

**変更後**
```cs
var message1 = "";
var message2 = "";
(cleared1, message1) = Proceed(1, cleared1, answer1, input);
(cleared2, message2) = Proceed(2, cleared2, answer2, input);
ViewData["Message"] = $"{message1}\n{message2}";
```

<hr />

[< 前へ](./textbook_advanced03.md) | [次へ >](./textbook_advanced05.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)