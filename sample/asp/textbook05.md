# ASP[]().NET Core アプリを作ってみよう

### 改善ポイント
1. 実はアプリを実行して最初に **「 → 数字を入力してください0 → 1-9 の数字を 1 文字入力してください0 → 答えはもっと大きい値です」** という表示がされてしまっています。これを消しましょう。  
複数ある ```if``` を ```else``` でつなぐことで、一か所だけが実行されるようになります。
1. テキスト入力ボックスは数値だけが入力できるように設定することができます。プレイヤーが入力する際に「数字を入力しなければならない」とわかる方ので設定した方が親切そうです。  
```<input type="text" name="text" ``` の ```type="text"``` を ```type="number"``` と変更しましょう。
1. テキスト入力ボックスへ入力した回答が **[ 回答 ] ボタン** を押すたびに消えてしまいます。これは消えてしまった方が便利か、消えないほうが便利化は好みが分かれるところですが、せっかくなので今回は残すようにしてみましょう。  
```ViewData["Answer"] = answer;``` の上に 1 行 ```ViewData["Text"] = text;``` を追加します。

**変更後 Index.chtml 例**
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

**変更後 HomeController.cs 例**
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
おめでとうございます！ これで数当てゲームの完成です。  
簡単なものではありますが、立派なアプリです。**C#** でアプリを作った経験者になりました。これから素晴らしい **C#** エンジニア生活を送ってください！

## 自習 - アプリの改善
今回作った数当てゲームはまだ不親切な部分があります。**C#** の学習としてぜひ自分で機能を追加してみてください。  

### 機能の例

- 正解を当てるまでにかかった回数を数えて表示する機能
- 1 ゲームごとに終了するのではなく、連続で遊べる機能
- 複数の答えを同時に当てていく機能
- 数字でなく単語を当てる機能

**あなたに、素晴らしい C# エンジニアライフを！**

<hr />

[< 前へ](./textbook04.md)

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
