# 進歩した WPF アプリを作ってみよう

## メソッドを使ったプログラムコード

ひとまず、**メソッド** を使ったプログラムコード (一部) が出来上がりました。現在のプログラムコードは次のようになっています。  

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
        var message1 = "";
        var message2 = "";
        (_cleared1, message1) = Proceed(1, _cleared1, _answer1, input);
        (_cleared2, message2) = Proceed(2, _cleared2, _answer2, input);
        Message = $"{message1}\n{message2}";
        if (!_cleared1 || !_cleared2)
        {
            return;
        }
        Message += "\nゲームクリア！\n終了するには右上の × ボタンを押してください... ";
        AnswerCommand.SetCanExecute(false);
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
}
```

正解の判定処理や、正解したか。クリア済みかなどの表示のプログラムコードが一回だけ書かれた状態になりました。これには次のようなメリットがあります。  

* クリア済みの表示を変更したいと思ったとします。これまでは 2 箇所を変更しなければなりませんでした。これからは 1 箇所の変更で済みます。これは変更の手間が少ないのみならず間違いにくいという効果もあります。  
* これまでのプログラムコードでは同じ変更を 2 箇所に対して行うので、どちらか一方で書き間違えをするリスクがありました。これからは 1 箇所の変更なので書き間違えをするリスクは劇的に減少します。  

この書き間違えリスクを減らすという考えはプログラミングにおいて最重要とも言える最優先事項です。覚えておきましょう。  
また、ゲームを 3 つに増やす場合にも、長いコードをコピー＆ペーストする必要がなくなります。

## まだ大きな書き間違えリスクが存在

**メソッド** でコードを整理しましたが、まだ書き換えやゲームを増やす際にミスをする大きなリスクが残っています。それが、```cleared1 = Proceed(1, cleared1, answer1, input);``` の ```cleared1 =```、```1, cleared1, answer1,``` の部分です。  
ゲームを 3 つに増やすそうと思ったら
```cs
(_cleared1, message1) = Proceed(1, _cleared1, _answer1, input);
(_cleared2, message2) = Proceed(2, _cleared2, _answer2, input);
```
となっている部分を 1 行コピー＆ペーストして
```cs
(_cleared1, message1) = Proceed(1, _cleared1, _answer1, input);
(_cleared2, message2) = Proceed(2, _cleared2, _answer2, input);
(_cleared2, message2) = Proceed(2, _cleared2, _answer2, input);
```
続いて 3 行目の ```2``` をすべて ```3``` と書き換えることになりますが、ここで書き換え漏れが発生します。どんなに注意深くプログラムコードを書いていても、撲滅はできないミスです。必ずどこかでミスをします。  

そこで **クラス** を使ってミスをする確率を減らしていきましょう。

## クラスを使ってみる

**クラス** を使ってプログラムコードを改善してみましょう。  
**※このコンテンツは入門者にプログラムコードを書いて動かしてもらうことを目的としているので、クラス の詳しいことは別途学習してください。**  

**クラス** を使うと複数のデータとプログラムの処理をひとまとめにして扱うことができるようになります。例えば今回のコードでは、```gameNo```、```cleared```、``` answer``` がゲーム 1 とゲーム 2 用で 2 セット必要になっていますし、それをセットでメソッドに渡すプログラムコードを書く際にミスも起こります。これを **クラス** を使って改善しましょう。

**変更前**
```cs
int _answer1;
int _answer2;
bool _cleared1 = false;
bool _cleared2 = false;
```
```cs
if (!_cleared1 || !_cleared2)
```
```cs
(_cleared1, message1) = Proceed(1, _cleared1, _answer1, input);
(_cleared2, message2) = Proceed(2, _cleared2, _answer2, input);
```

**クラス** を追加します。
```cs
internal class Game
{
    int _no { get; init; }
    int _answer { get; init; }
    public bool Cleared { get; private set; }

    public Game(int no)
    {
        _no = no;
        _answer = new System.Random().Next(1, 9);
    }

    public string Proceed(int input)
    {
        var no = $"ゲーム{_no}: ";
        if (Cleared)
        {
            return $"{input} → {no}クリア済みです";
        }
        if (input > _answer)
        {
            return $"{input} → {no}答えはもっと小さい値です";
        }
        if (input < _answer)
        {
            return $"{input} → {no}答えはもっと大きい値です";
        }
        Cleared = true;
        return $"{input} → {no}正解！ クリアです";
    }
}
```

ゲーム 1、ゲーム 2 の処理をこの **クラス** を使うように変更します。

<hr />

[< 前へ](./textbook_advanced04.md) | [次へ >](./textbook_advanced06.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)