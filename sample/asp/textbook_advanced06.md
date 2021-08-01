# 進歩した ASP[]().NET Core アプリを作ってみよう

## 変更後のプログラムコード

**クラス** を使うように変更したプログラムコードはこのようになります。

**変更後**  
**Index.cshtml**
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
**HomeController**
```cs
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
```

また、```Proceed``` メソッドは **クラス** の中に書いたので、これまでの ```Proceed``` メソッドは削除します。  

## クラスを使ったプログラムコード

ひとまず、**クラス** を使ったプログラムコードが出来上がりました。現在のプログラムコードは次のようになっています。
**Index.cshtml**
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

## クラスを別のファイルに書く

**メソッド**、**クラス** を使ってプログラムコードを改善してきました。しかしこれまでの知識だけでは大規模なアプリを作る際に大きな課題に直面します。その課題とは、プログラムコードを一つのファイルに書いていることです。複雑な処理を行うアプリになると、何十万行・何百万行とプログラムコードも大きくなっていきます。それを一つのファイルにすべて書くというのは非現実的です。  
一つのファイルにどのくらいの行数のプログラムコードがかかれるのが適切かは状況によって異なりますが、どのような状況でも一つのファイルが 400 行を超えるようだと何かが間違っていると考えた方が良いです。  

## 新しいファイルを作成する

**ErrorViewModel.cs** と同じディレクトリ(**※1**)に新しいファイルを作成します。作成方法はなんでも良い(**※2**)のですが今回は Visual Studio Code から作ってみます。  

![image](./image0002.png)

- Visual Studio Code のエクスプローラー ペインで **AspApp**、**Models** と選択します。
- Visual Studio Code のエクスプローラー ペインでマウスカーソルをかさねると「新しいファイル」と表示されるアイコンをクリックします。
- **IndexViewModel.cs** と入力し、Enter キーを押します。

これでファイルが作られて Visual Studio Code で開かれます。簡単ですね。  

**※1**： 同一ディレクトリでなくとも配下のディレクトリであれば読み込んでくれます。規模が大きくなったらディレクトリで整理をすることになります。  
**※2**： そこにファイルがあれば読み込んでくれるので、他の場所からコピーする・ターミナル(端末)で作る・エクスプローラー/Finder で作る、などどのような方法で作ってもかまいません。  

## ファイル名について

C# のプログラムコードを書くファイルのファイル名は拡張子を **.cs** にする必要がます。名前は自由につけて構いません。  

パソコンを買って買ったままの設定で使っていると、拡張子が見えない状態になっているかもしれません。  
インターネット検索などで、拡張子を表示する方法を探して設定を行っておくのがオススメです。プログラミングを行う際には拡張子が非常に重要で常に意識しておく必要があります。また、日常でパソコンを使っている場合にはあまりないかもしれませんが、拡張子を変更する機会もかなりの頻度で発生します。IT エンジニアがパソコンを買ってまず真っ先に設定するものと言って過言ではない設定の一つです。

<hr />

[< 前へ](./textbook_advanced05.md) | [次へ >](./textbook_advanced07.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)