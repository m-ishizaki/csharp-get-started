# 進歩した ASP[]().NET Core アプリを作ってみよう

## メソッドを使ったプログラムコード

ひとまず、**メソッド** を使ったプログラムコードが出来上がりました。現在のプログラムコード  (一部)  は次のようになっています。  

```cs
public IActionResult Index(string text, int answer1, int answer2, bool cleared1, bool leared2)
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
        var message1 = "";
        var message2 = "";
        (cleared1, message1) = Proceed(1, cleared1, answer1, input);
        (cleared2, message2) = Proceed(2, cleared2, answer2, input);
        ViewData["Message"] = $"{message1}\n{message2}";
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

正解の判定処理や、正解したか。クリア済みかなどの表示のプログラムコードが一回だけ書かれた状態になりました。これには次のようなメリットがあります。  

* クリア済みの表示を変更したいと思ったとします。これまでは 2 箇所を変更しなければなりませんでした。これからは 1 箇所の変更で済みます。これは変更の手間が少ないのみならず間違いにくいという効果もあります。  
* これまでのプログラムコードでは同じ変更を 2 箇所に対して行うので、どちらか一方で書き間違えをするリスクがありました。これからは 1 箇所の変更なので書き間違えをするリスクは劇的に減少します。  

この書き間違えリスクを減らすという考えはプログラミングにおいて最重要とも言える最優先事項です。覚えておきましょう。  
また、ゲームを 3 つに増やす場合にも、長いコードをコピー＆ペーストする必要がなくなります。

## まだ大きな書き間違えリスクが存在

**メソッド** でコードを整理しましたが、まだ書き換えやゲームを増やす際にミスをする大きなリスクが残っています。それが、```(cleared1, message1) = Proceed(1, cleared1, answer1, input);``` の ```cleared1 =```、```1, cleared1, answer1,``` の部分です。  
ゲームを 3 つに増やすそうと思ったら
```cs
(cleared1, message1) = Proceed(1, cleared1, answer1, input);
(cleared2, message2) = Proceed(2, cleared2, answer2, input);
```
となっている部分を 1 行コピー＆ペーストして
```cs
(cleared1, message1) = Proceed(1, cleared1, answer1, input);
(cleared2, message2) = Proceed(2, cleared2, answer2, input);
(cleared2, message2) = Proceed(2, cleared2, answer2, input);
```
続いて 3 行目の ```2``` をすべて ```3``` と書き換えることになりますが、ここで書き換え漏れが発生します。どんなに注意深くプログラムコードを書いていても、撲滅はできないミスです。必ずどこかでミスをします。  

そこで **クラス** を使ってミスをする確率を減らしていきましょう。

## クラスを使ってみる

**クラス** を使ってプログラムコードを改善してみましょう。  
**※このコンテンツは入門者にプログラムコードを書いて動かしてもらうことを目的としているので、クラス の詳しいことは別途学習してください。**  

**クラス** を使うと複数のデータとプログラムの処理をひとまとめにして扱うことができるようになります。例えば今回のコードでは、```gameNo```、```cleared```、``` answer``` がゲーム 1 とゲーム 2 用で 2 セット必要になっていますし、それをセットでメソッドに渡すプログラムコードを書く際にミスも起こります。これを **クラス** を使って改善しましょう。

#### **Index.cshtml**
**変更前**
```html
    <input type="number" name="text" value="@ViewData["Text"]"/><br/>
    <button type="submit">回答</button><br/>
    <input type="hidden" name="answer1" value="@ViewData["Answer1"]" />
    <input type="hidden" name="answer2" value="@ViewData["Answer2"]" />
    <input type="hidden" name="cleared1" value="@ViewData["Cleared1"].ToString()" />
    <input type="hidden" name="cleared2" value="@ViewData["Cleared2"].ToString()" />
</form>
<pre>@ViewData["Message"]</pre>
```

#### **HomeController.cs**
**変更前**
```cs
public IActionResult Index(string text, int answer1, int answer2, bool cleared1, bool cleared2)
```
```cs
if (!(answer1 > 0 && answer1 < 10))
{
    answer1 = new System.Random().Next(1, 9);
    answer2 = new System.Random().Next(1, 9);
}
```
```cs
var message1 = "";
var message2 = "";
(cleared1, message1) = Proceed(1, cleared1, answer1, input);
(cleared2, message2) = Proceed(2, cleared2, answer2, input);
ViewData["Message"] = $"{message1}\n{message2}";
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
```

**クラス** を追加します。
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

ゲーム 1、ゲーム 2 の処理をこの **クラス** を使うように変更します。

<hr />

[< 前へ](./textbook_advanced04.md) | [次へ >](./textbook_advanced06.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)