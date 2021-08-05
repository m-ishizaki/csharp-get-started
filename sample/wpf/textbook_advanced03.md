# 進歩した WPF アプリを作ってみよう

## 二つのゲームを同時に進行する
まずは、**クラス**、**メソッド** を使わずに二つのゲームを進行するように変更してみましょう。  

### 答えを二つ作る
答えを二つ作るように変更します。  

**変更前**
```cs
int _answer;
```
```cs
_answer = new System.Random().Next(1, 9);
```
**変更後**
```cs
int _answer1;
int _answer2;
```
```cs
_answer1 = new System.Random().Next(1, 9);
_answer2 = new System.Random().Next(1, 9);
```

### ゲームのクリアを記録する
元のアプリではゲームをクリアしたらすぐに終了する処理になっていましたが、今度は二つともをクリアしなければ終了の処理にならないようにしなければなりません。また、ゲームを一つだけクリアした状態ではクリアした方の表示も異なります。  
つまり、ゲームそれぞれについてクリアをしたかどうかをアプリが記憶できるようにしなければなりません。  

そこで、ゲームクリアフラグを二つ追加します。  

**変更前**
```cs
int _answer1;
int _answer2;
```
**変更語**
```cs
int _answer1;
int _answer2;
bool _cleared1 = false;
bool _cleared2 = false;
```

### ゲームのクリア判定を変更する
ゲームのクリア判定を「二つのゲームがクリアされた場合」という条件に変更します。  

**変更前**
```cs
Message = "正解！\n終了するには右上の × ボタンを押してください... ";
```
**変更後**
```cs
if (!_cleared1 || !_cleared2)
{
    return;
}
Message += "\nゲームクリア！\n終了するには右上の × ボタンを押してください... ";
```
また、```!``` は否定を意味します。そして ```||``` は **OR** を意味します。つまりこのプログラムコードは「```_cleared1``` が **true** でない、または、```_cleared2``` が **true** でない場合」という意味になります。  
もう少し自然言語に意訳すると「両方のゲームがクリアされるまでクリアにならない」です。プログラミングでは、このように自然言語で考えた場合を大きく意訳したプログラムコードを書くことになることが多くあります。柔軟に考える力が重要です。  

### ゲーム 1 がクリア済みの場合の処理
ゲームが片一方だけクリアされている状態の処理を作っていきます。まず、ゲーム 1 がクリア済みの場合の処理です。クリア済みの場合「**ゲーム1: クリア済みです**」と表示します。  

**変更前**
```cs
if (input > _answer)
{
    Message = $"{input} → 答えはもっと小さい値です";
    return;
}
if (input < _answer)
{
    Message = $"{input} → 答えはもっと大きい値です";
    return;
}
```
**変更後**
```cs
Message = "";
if (_cleared1)
{
    Message += $"{input} → ゲーム1: クリア済みです";
}
else if (input > _answer1)
{
    Message += $"{input} → ゲーム1: 答えはもっと小さい値です";
}
else if (input < _answer1)
{
    Message += $"{input} → ゲーム1: 答えはもっと大きい値です";
}
```

### ゲーム 1 の正解の判定を追加する

ゲーム 1 の正解の判定を追加します。具体的な処理としては、正解した場合に ```_cleared1``` を **true** にして、正解しなかった場合に答えのヒントを表示するようにします。

**変更前**
```cs
Message = "";
if (_cleared1)
{
    Message += $"{input} → ゲーム1: クリア済みです";
}
else if (input > _answer1)
{
    Message += $"{input} → ゲーム1: 答えはもっと小さい値です";
}
else if (input < _answer1)
{
    Message += $"{input} → ゲーム1: 答えはもっと大きい値です";
}
```
**変更後**
```cs
Message = "";
if (_cleared1)
{
    Message += $"{input} → ゲーム1: クリア済みです";
}
else if(input == _answer1)
{
    Message += $"{input} → ゲーム1: 正解！ クリアです";
    _cleared1 = true;
}
else if (input > _answer1)
{
    Message += $"{input} → ゲーム1: 答えはもっと小さい値です";
}
else if (input < _answer1)
{
    Message += $"{input} → ゲーム1: 答えはもっと大きい値です";
}
```

### ゲーム 2 の処理を追加する

ゲーム 1 と同様の処理を行う、ゲーム 2 用のプログラムコードを追加します。

**追加するコード**
```cs
Message += "\n";
if (_cleared2)
{
    Message += $"{input} → ゲーム2: クリア済みです";
}
else if(input == _answer2)
{
    Message += $"{input} → ゲーム2: 正解！ クリアです";
    _cleared2 = true;
}
else if (input > _answer2)
{
    Message += $"{input} → ゲーム2: 答えはもっと小さい値です";
}
else if (input < _answer2)
{
    Message += $"{input} → ゲーム2: 答えはもっと大きい値です";
}
```

<hr />

[< 前へ](./textbook_advanced02.md) | [次へ >](./textbook_advanced04.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)