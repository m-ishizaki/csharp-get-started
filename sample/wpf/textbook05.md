# WPF アプリを作ってみよう

### プレイヤーが正解を入力できたかを確認する

```if(...){ } else { }``` を使えばアプリが様々な判断をして動作を変えられることがわかりました。プレイヤーが入力した答えが正解かどうかを判断する機能もこの ```if(...){ } else { }``` で作れます。作っていきましょう。  
また、出力が見やすくなるように ```Message += "\n";``` という 1 行も追加しています。```\n``` は **C#** を含めた多くのプログラムで **改行** を表します。
```cs
Message += "\n";
if (input > _answer)
{
    Message += input + " → 答えはもっと小さい値です";
}
if (input < _answer)
{
    Message += input + " → 答えはもっと大きい値です";
}
if (input == _answer)
{
    Message += input + " → 正解！\n終了するには右上の × ボタンを押してください... ";
}
```
コードを変更した **MainWindow.xaml.cs (の一部)** は次のようになります。  
```cs
class MainWindowViewModel : BaseViewModel
{
    public Command AnswerCommand { get; }
    public string Input { get; set; }
    int _answer;
    string _message;
    public string Message
    {
        get { return _message; }
        set { SetProperty(ref _message, value); }
    }

    public MainWindowViewModel()
    {
        AnswerCommand = new Command(OnAnswerCommand);
        _answer = new System.Random().Next(1, 9);
    }

    void OnAnswerCommand(object parameter)
    {
        if (!int.TryParse(Input, out var input))
        {
            Message = Input + " → 数字を入力してください";
        } else {
            Message = Input + " → 数値が入力されました";
        }
        if (input < 1 || input > 9)
        {
            Message += Input + " → 1-9 の数字を 1 文字入力してください";
        }
        Message += "\n";
        if (input > _answer)
        {
            Message += input + " → 答えはもっと小さい値です";
        }
        if (input < _answer)
        {
            Message += input + " → 答えはもっと大きい値です";
        }
        if (input == _answer)
        {
            Message += input + " → 正解！\n終了するには右上の × ボタンを押してください... ";
        }
    }
}
```
実行してみましょう。正解を入力できると
```
正解！\n終了するには右上の × ボタンを押してください... 
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

### 正解が入力されたらゲームを終える

今のままではプレイヤーが正解を当てた後もずっと回答を続けられます。正解が分かっているのに回答を続けても仕方がありません。正解を当てたらもう回答をできないようにしてみましょう。  
正解をした際の処理の ```Message += input + " → 正解！\n終了するには右上の × ボタンを押してください... ";``` の次の行に、次の 1 行を追加します。
```cs
AnswerCommand.SetCanExecute(false);
```
この 1 行で、**AnswerCommand** を以降は実行できなくできます。 **AnswerCommand** は **[ 回答 ] ボタン** と関連付いているので、**[ 回答 ] ボタン** が使用不可に (グレーになってクリックしても反応しなく) なります。  

実行してみましょう。正解を入力できると **[ 回答 ] ボタン** が使用不可になれば成功です。  

これで、ゲームとして成立するアプリが完成したといってもよいでしょう。  
しかし、少し処理をわかりやすくするためのコードの改善をしてみましょう。

<hr />

[< 前へ](./textbook04.md) | [次へ >](./textbook06.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
<hr />
