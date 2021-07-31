# 進歩した WPF アプリを作ってみよう

## 二つのゲームを同時に進行するプログラムコード

ひとまず、**クラス**、**メソッド** を使わずに二つのゲームを進行するプログラムコードが出来上がりました。現在のプログラムコード (一部) は次のようになっています。  

```cs
class MainWindowViewModel : BaseViewModel
{
    public Command AnswerCommand { get; }
    public string Input { get; set; }
    int _answer1;
    int _answer2;
    bool _cleared1 = false;
    bool _cleared2 = false;
    string _message;
    public string Message
    {
        get { return _message; }
        set { SetProperty(ref _message, value); }
    }

    public MainWindowViewModel()
    {
        AnswerCommand = new Command(OnAnswerCommand);
        _answer1 = new System.Random().Next(1, 9);
        _answer2 = new System.Random().Next(1, 9);
    }

    void OnAnswerCommand(object parameter)
    {
        if (!int.TryParse(Input, out var input))
        {
            Message = $"{Input} → 数字を入力してください";
            return;
        }
        if (input < 1 || input > 9)
        {
            Message = $"{input} → 1-9 の数字を 1 文字入力してください";
            return;
        }
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
        if (!_cleared1 || !_cleared2)
        {
            return;
        }
        Message += "\nゲームクリア！\n終了するには右上の × ボタンを押してください... ";
        AnswerCommand.SetCanExecute(false);
    }
}
```

## メソッドを使ってみる

**メソッド** を使ってプログラムコードを改善してみましょう。  
**※このコンテンツは入門者にプログラムコードを書いて動かしてもらうことを目的としているので、メソッド の詳しいことは別途学習してください。**  

**メソッド** を使うとある程度のプログラムの処理をひとまとめにして扱うことができるようになります。わかりやすい例としては、複数回書かれているコードの組を 1 度書くだけで良くなります。例えば今回のコードでは、次の部分が、ゲーム 1 とゲーム 2 用でほぼ同じコードが 2 回書かれています。これを **メソッド** にしてみましょう。  

**変更前**
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

**メソッド** を追加します。

```cs
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

ゲーム 1、ゲーム 2 の処理をこの **メソッド** を使うように変更します。

**変更後**
```cs
var message1 = "";
var message2 = "";
(_cleared1, message1) = Proceed(1, _cleared1, _answer1, input);
(_cleared2, message2) = Proceed(2, _cleared2, _answer2, input);
Message = $"{message1}\n{message2}";
```

<hr />

[< 前へ](./textbook_advanced03.md) | [次へ >](./textbook_advanced05.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)