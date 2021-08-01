# 進歩した ASP[]().NET Core アプリを作ってみよう

## クラスを別のファイルに書く

先ほど作った新しいファイル **IndexViewModel.cs** にクラスのプログラムコードを移動しましょう。  
移動するのは ```IndexViewModel```、```Game``` クラスのコードです。  

**変更前**
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

**変更後 HomeController.cs**
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

**変更後 IndexViewModel.cs**
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

<hr />

[< 前へ](./textbook_advanced06.md) | [次へ >](./textbook_advanced08.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)