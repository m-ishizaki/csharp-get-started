# ASP[]().NET Core アプリを作ってみよう

### 正解が入力されるまで繰り返す

今のままではプレイヤーに与えられたチャンスは 1 回だけです。数を当てられなかった場合に「もっと小さい」「もっと大きい」というヒントを得られますが、それを活かす機会は与えられません。完全にエスパー養成アプリになってしまっています。そこで正解するまで繰り返しチャレンジできる処理を追加します。  

今は **[ 回答 ] ボタン** を押すたびに ```answer``` を作り直していますが、これを一回作ったらずっと使えるようにします。変更箇所は 4 箇所です。  

#### 1 箇所目
**変更前**  
```cs
public IActionResult Index(string text)
```

**変更後**
```cs
public IActionResult Index(string text, int answer)
```

#### 2 箇所目
**変更前**  
```cs
var answer = new System.Random().Next(1, 9);
```

**変更後**
```cs
if (!(answer > 0 && answer < 10))
{
    answer = new System.Random().Next(1, 9);
}
```

#### 3 箇所目
```cs
return View();
```
の上に 1 行追加します。  

**追加**
```cs
ViewData["Answer"] = answer;
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
これで、プレイヤーが正解を入力するまで繰り返す処理が完成です。ゲームとしても完成したといってもよいでしょう。  
しかし、少し処理をわかりやすくするためのコードの改善をしてみましょう。

<hr />

[< 前へ](./textbook03.md) | [次へ >](./textbook05.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
