# ASP[]().NET Core アプリを作ってみよう

### 入力された文字列が 1 桁の数値であるかを確認する

```if (...){ } else { }``` で条件によって実行されるコードを分岐できることを学びました。  
今度はこの機能をうまく使って、入力された文字列が 1 桁の数値であるかを確認してみましょう。
```cs
if (input < 1 || input > 9)
{
    ViewData["Message"] += $"{input} → 1-9 の数字を 1 文字入力してください";
}
```
入力された文字列を数値にした ```input``` が、1 より小さいまたは 9 より大きい時だけ **1-9 の数字を 1 文字入力してください** と表示するコードです。 ```input < 1``` といった書き方で、「1 より小さい場合」といった条件を表します。 ```||``` は or 条件です。「1 より小さい」「or」「9 より大きい」場合にだけ ```{ }``` の中のコードが実行されます。  
すなわち、1 ～ 9 の数値 **でない** だけ実行されます。これによりアプリが求める入力の書式に合わないプレイヤーの入力に対して、**「違うよ」** というメッセージをプレイヤーに伝えることができます。  

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

        public IActionResult Index(string text)
        {
            var answer = new System.Random().Next(1, 9);
            if (!int.TryParse(text, out var input))
            {
                ViewData["Message"] = $"{text} → 数字を入力してください";
            } else {
                ViewData["Message"] = $"{text} → 数値が入力されました";
            }
            if (input < 1 || input > 9)
            {
                ViewData["Message"] += $"{input} → 1-9 の数字を 1 文字入力してください";
            }
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
実行し、キーボードで **12** と打って Enter キーを押してみましょう。
```
12 → 数値が入力されました12 → 1-9 の数字を 1 文字入力してください
```
と表示されます。再度実行してこんどは **3** と打って Enter キーを押してみましょう。  
```
3 → 数値が入力されました
```
と表示されます。

### プレイヤーが正解を入力できたかを確認する

```if(...){ } else { }``` を使えばアプリが様々な判断をして動作を変えられることがわかりました。プレイヤーが入力した答えが正解かどうかを判断する機能もこの ```if(...){ } else { }``` で作れます。作っていきましょう。  
```cs
if (input > answer)
{
    ViewData["Message"] += $"{input} → 答えはもっと小さい値です";
}
if (input < answer)
{
    ViewData["Message"] += $"{input} → 答えはもっと大きい値です";
}
if (input == answer)
{
    ViewData["Message"] += $"{input} → 正解！ ";
}
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

        public IActionResult Index(string text)
        {
            var answer = new System.Random().Next(1, 9);
            if (!int.TryParse(text, out var input))
            {
                ViewData["Message"] = $"{text} → 数字を入力してください";
            }
            else
            {
                ViewData["Message"] = $"{text} → 数値が入力されました";
            }
            if (input < 1 || input > 9)
            {
                ViewData["Message"] += $"{input} → 1-9 の数字を 1 文字入力してください";
            }
            if (input > answer)
            {
                ViewData["Message"] += $"{input} → 答えはもっと小さい値です";
            }
            if (input < answer)
            {
                ViewData["Message"] += $"{input} → 答えはもっと大きい値です";
            }
            if (input == answer)
            {
                ViewData["Message"] += $"{input} → 正解！ ";
            }
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
実行してみましょう。正解を入力できると
```
正解！ 
```
と表示されます。正解できない場合は
```
答えはもっと小さい値です
```
または
```
答えはもっと大きい値です
```
と表示されます。ゲームらしさが出てきましたね。

<hr />

[< 前へ](./textbook02.md) | [次へ >](./textbook04.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
