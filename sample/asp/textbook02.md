# ASP[]().NET Core アプリを作ってみよう

### ランダムな数字を作る
数字あてゲームのためにはランダムな数字が必要です。  
次のように書いてランダムな数値を作ります。
```cs
var answer = new System.Random().Next(1, 9);
```
この 1 行で、1～9 のランダムが数字が作られ ```answer``` という名前でプログラムの中で使えるようになります。  
この ```answer``` という名前のランダムで作られた数字はプログラムで表示させることができます。  
```cs
ViewData["Message"] = answer;
```
二つのコードを追加した **HomeController.cs** は次のようになります。  
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

        public IActionResult Index()
        {
            var answer = new System.Random().Next(1, 9);
            ViewData["Message"] = answer;
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
実行すると **[ 回答 ]** ボタンの下に
```
{1～9 のランダムな数字}
```
が表示されます。

### 文字を入力する
**[ 回答 ]** ボタンの上のテキスト入力ボックスはプレイヤーがキーボードから文字を入力できます。ここに入力された文字をプログラムコードで使えるように、次のように書いてプログラムが文字の入力を受け付けるようにしましょう。
**変更前**
```cs
public IActionResult Index()
```
**変更後**
```cs
public IActionResult Index(string text)
```
プレイヤーのキーボードからの入力を受け付け ```text``` という名前でプログラムの中で使えるようになります。  
この ```text``` をプログラムで表示させてみましょう。
**変更前**
```cs
ViewData["Message"] = answer;
```
**変更後**
```cs
ViewData["Message"] = text;
```
二つのコードを変更した **HomeController.cs** は次のようになります。  
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
            ViewData["Message"] = text;
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
実行し、テキスト入力ボックスに、例えばキーボードで **suuji** と打って **[ 回答 ]** ボタンを押してみましょう。
```
suuji
```
と表示されます。

### 入力された文字を数値にする

プレイヤーが答えをキーボードを入力するための入力受け付け機能に使うプログラムコードはできましたが、このままでは入力を数値として扱えません。  
プログラムでは数値と数字はまったくの別物になるので気を付けましょう。例えば、1 という数値と 2 という数値を足すと 1 + 2 = 3 で 3 という数値になります。しかし、数字の場合は 1 + 2 = 12 と一文字 + 一文字 = 二文字の文字列となりまったくの別物です。
今回は **C#** には文字列を数値にするための機能があるのでそれを使っていきます。
```cs
if (!int.TryParse(text, out var input))
{
    ViewData["Message"] = "数字を入力してください";
} else {
    ViewData["Message"] = "数値が入力されました";
}
```
このコードで入力された文字がを数値にしたものが ```input``` という名前で使えるようになります。また数値にできなかった場合は、```if``` と ```else``` の間の
```cs
ViewData["Message"] = "数字を入力してください";
```
が実行されます。この時、```else``` の次の
```cs
ViewData["Message"] = "数値が入力されました";
```
は実行されません。数値にできた場合は、
```cs
ViewData["Message"] = "数字を入力してください";
```
は実行されずに
```cs
ViewData["Message"] = "数値が入力されました";
```
が実行されます。```if(...){ } else { }``` はこのように条件によってどちら一方だけを実行したい場合に使える機能です。  
**※実際には ```else``` がなかったり ```{ }``` がなかったりという使い方が多いので、```if``` については別途しっかり学んでください。**  

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
                ViewData["Message"] = "数字を入力してください";
            } else {
                ViewData["Message"] = "数値が入力されました";
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
実行し、テキスト入力ボックスにキーボードで **suuji** と打って **[ 回答 ]** ボタンを押してみましょう。
```
数字を入力してください
```
と表示されます。再度実行して今度は **3** と打って **[ 回答 ]** ボタンを押してみましょう。  
```
3
```
と表示されます。
<hr />

[< 前へ](./textbook01.md) | [次へ >](./textbook03.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
