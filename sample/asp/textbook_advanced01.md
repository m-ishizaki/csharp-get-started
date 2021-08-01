# 進歩した ASP[]().NET Core アプリを作ってみよう

## 以前のプログラムコード

まずは、[ASP.NET アプリケーションを作ってみよう](./textbook.md) で作ったプログラムコードを開きます。開き方もリンク先のテキストを参照してください。  
もし、リンク先のコンテンツは読んだだけで理解できたのでいきなり進歩したアプリから始めたいという場合は、リンク先のテキストに **完成サンプルコード** もあります。

**現在のプログラムコード**  
現在のプログラムコード ( **HomeController.cs** ) は次のようになっているはずです。  
このコードを **メソッド**、**クラス** という機能を使って進歩させていきます ( 実際には結構なメソッド、クラスを使っていますが、改めて意識して使っていきましょう )。
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

        public IActionResult Index(string text, int answer)
        {
            if (!(answer > 0 && answer < 10))
            {
                answer = new System.Random().Next(1, 9);
            }
            else if (!int.TryParse(text, out var input))
            {
                ViewData["Message"] = $"{text} → 数字を入力してください";
            }
            else if (input < 1 || input > 9)
            {
                ViewData["Message"] = $"{input} → 1-9 の数字を 1 文字入力してください";
            }
            else if (input > answer)
            {
                ViewData["Message"] = $"{input} → 答えはもっと小さい値です";
            }
            else if (input < answer)
            {
                ViewData["Message"] = $"{input} → 答えはもっと大きい値です";
            }
            else
            {
                ViewData["Message"] = $"{input} → 正解！ ";
            }
            ViewData["Text"] = text;
            ViewData["Answer"] = answer;
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

## 完成コード
いったんはアプリを完成させたことがある経験者が前提となるので、今回は最初に完成コードを一度見ておきましょう。  
[完成サンプルコード](./src_advanced)  
**IndexViewModel.cs**
```cs
namespace AspApp.Models
{
    public class IndexViewModel
    {
        public string Text { get; set; }
        public string Message { get; set; }
        public Game Game1 { get; } = new Game(1);
        public Game Game2 { get; } = new Game(2);
    }

    public class Game
    {
        public int Answer { get; set; }
        public bool Cleared { get; set; }

        int _no { get; init; }

        public Game(int no)
        {
            _no = no;
        }

        public bool HasAnswer()
        {
            if (!(Answer > 0 && Answer < 10))
            {
                return false;
            }
            return true;
        }

        public string Proceed(int input)
        {
            var no = $"ゲーム{_no}: ";
            if (Cleared)
            {
                return $"{input} → {no}クリア済みです";
            }
            if (input > Answer)
            {
                return $"{input} → {no}答えはもっと小さい値です";
            }
            if (input < Answer)
            {
                return $"{input} → {no}答えはもっと大きい値です";
            }
            Cleared = true;
            return $"{input} → {no}正解！ クリアです";
        }
    }
}
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

        public IActionResult Index(IndexViewModel model)
        {
            if (!model.Game1.HasAnswer() || !model.Game2.HasAnswer())
            {
                model.Game1.Answer = new System.Random().Next(1, 9);
                model.Game2.Answer = new System.Random().Next(1, 9);
            }
            else if (!int.TryParse(model.Text, out var input))
            {
                model.Message = $"{model.Text} → 数字を入力してください";
            }
            else if (input < 1 || input > 9)
            {
                model.Message = $"{input} → 1-9 の数字を 1 文字入力してください";
            }
            else
            {
                model.Message = $"{model.Game1.Proceed(input)}\n{model.Game2.Proceed(input)}";
            }
            if (model.Game1.Cleared && model.Game2.Cleared)
            {
                model.Message += "\nゲームクリア！ ";
            }
            return View(model);
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
**Index.chtml**
```html
@model AspApp.Models.IndexViewModel
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
        <input type="number" name="text" value="@Model.Text"/><br/>
        <button type="submit">回答</button><br/>
        <input type="hidden" name="game1.answer" value="@Model.Game1.Answer" />
        <input type="hidden" name="game2.answer" value="@Model.Game2.Answer" />
        <input type="hidden" name="game1.cleared" value="@Model.Game1.Cleared.ToString()" />
        <input type="hidden" name="game2.cleared" value="@Model.Game2.Cleared.ToString()" />
    </form>
    <pre>@Model.Message</pre>
</div>
```

<hr />

[< 前へ](./textbook_advanced.md) | [次へ >](./textbook_advanced02.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)