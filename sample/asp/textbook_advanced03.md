# 進歩した ASP[]().NET Core アプリを作ってみよう

## 二つのゲームを同時に進行する
まずは、**クラス**、**メソッド** を使わずに二つのゲームを進行するように変更してみましょう。  

### 答えを二つ作る
答えを二つ作るように変更します。  

#### **Index.chtml**
**変更前**
```html
<input type="hidden" name="answer" value="@ViewData["Answer"]" />
```
**変更後**
```html
<input type="hidden" name="answer1" value="@ViewData["Answer1"]" />
<input type="hidden" name="answer2" value="@ViewData["Answer2"]" />
```

#### **HomeController.cs**
**変更前**
```cs
public IActionResult Index(string text, int answer)
```
```cs
if (!(answer > 0 && answer < 10))
{
    answer = new System.Random().Next(1, 9);
}
```
```cs
ViewData["Answer"] = answer;
```
**変更後**
```cs
public IActionResult Index(string text, int answer1, int answer2)
```
```cs
if (!(answer1 > 0 && answer1 < 10))
{
    answer1 = new System.Random().Next(1, 9);
    answer2 = new System.Random().Next(1, 9);
}
```
```cs
ViewData["Answer1"] = answer1;
ViewData["Answer2"] = answer2;
```

### ゲームのクリアを記録する
元のアプリではゲームをクリアしたらすぐに終了する処理になっていましたが、今度は二つともをクリアしなければ終了の処理にならないようにしなければなりません。また、ゲームを一つだけクリアした状態ではクリアした方の表示も異なります。  
つまり、ゲームそれぞれについてクリアをしたかどうかをアプリが記憶できるようにしなければなりません。  

そこで、ゲームクリアフラグを二つ追加します。  

#### **Index.chtml**
**変更前**
```html
<input type="hidden" name="answer1" value="@ViewData["Answer1"]" />
<input type="hidden" name="answer2" value="@ViewData["Answer2"]" />
```
**変更後**
```html
<input type="hidden" name="answer1" value="@ViewData["Answer1"]" />
<input type="hidden" name="answer2" value="@ViewData["Answer2"]" />
<input type="hidden" name="cleared1" value="@ViewData["Cleared1"].ToString()" />
<input type="hidden" name="cleared2" value="@ViewData["Cleared2"].ToString()" />
```

#### **HomeController.cs**
**変更前**
```cs
public IActionResult Index(string text, int answer1, int answer2)
```
**変更後**
```cs
public IActionResult Index(string text, int answer1, int answer2, bool cleared1, bool cleared2)
```

### ゲームのクリア判定を変更する
ゲームのクリア判定を「二つのゲームがクリアされた場合」という条件に変更します。  

**変更前**
```cs
else
{
    ViewData["Message"] = $"{input} → 正解！ ";
}
```
**変更後**
```cs
if (cleared1 && cleared2)
{
    ViewData["Message"] += "\nゲームクリア！ ";
}
```

### ゲーム 1 がクリア済みの場合の処理
ゲームが片一方だけクリアされている状態の処理を作っていきます。まず、ゲーム 1 がクリア済みの場合の処理です。クリア済みの場合「**ゲーム1: クリア済みです**」と表示します。  

**変更前**
```cs
else if (input > answer)
{
    ViewData["Message"] = $"{input} → 答えはもっと小さい値です";
}
else if (input < answer)
{
    ViewData["Message"] = $"{input} → 答えはもっと大きい値です";
}
```
**変更後**
```cs
else
{
    if (cleared1)
    {
        ViewData["Message"] += $"{input} → ゲーム1: クリア済みです";
    }
    else
    {
        if (input > answer1)
        {
            ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと小さい値です";
        }
        else if (input < answer1)
        {
            ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと大きい値です";
        }
    }
}
```

### ゲーム 1 の正解の判定を追加する

ゲーム 1 の正解の判定を追加します。具体的な処理としては、正解した場合に ```cleared1``` を **true** にして、正解しなかった場合に答えのヒントを表示するようにします。

**変更前**
```cs
else
{
    if (cleared1)
    {
        ViewData["Message"] += $"{input} → ゲーム1: クリア済みです";
    }
    else
    {
        if (input > answer1)
        {
            ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと小さい値です";
        }
        else if (input < answer1)
        {
            ViewData["Message"] += $"{input} → ゲーム1: 答えはもっと大きい値です";
        }
    }
}
```
```cs
ViewData["Text"] = text;
ViewData["Answer1"] = answer1;
ViewData["Answer2"] = answer2;
```
**変更後**
```cs
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
}
```
```cs
ViewData["Text"] = text;
ViewData["Answer1"] = answer1;
ViewData["Answer2"] = answer2;
ViewData["Cleared1"] = cleared1;
```

### ゲーム 2 の処理を追加する

ゲーム 1 と同様の処理を行う、ゲーム 2 用のプログラムコードを追加します。

**追加するコード**
```cs
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
```cs
ViewData["Cleared2"] = cleared2;
```

<hr />

[< 前へ](./textbook_advanced02.md) | [次へ >](./textbook_advanced04.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)