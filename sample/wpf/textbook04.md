# WPF アプリを作ってみよう

### 入力された文字を数値にする

プレイヤーが答えを入力するための入力受け付け機能に使うプログラムコードはできましたが、このままでは数字でない文字も入力できてしまいます。そこで受け付ける入力を数値とみなせる文字列だけにしてみます。  
プログラムでは数値と数字はまったくの別物になるので気を付けましょう。例えば、1 という数値と 2 という数値を足すと 1 + 2 = 3 で 3 という数値になります。しかし、数字の場合は 1 + 2 = 12 と一文字 + 一文字 = 二文字の文字列となりまったくの別物です。
今回は C# には文字列を数値にするための機能があるのでそれを使っていきます。数字だけが入力されていれば、その入力は数値にできるので簡易的な入力のチェックができます。
```cs
if (!int.TryParse(Input, out var input))
{
    Message = Input + " → 数字を入力してください";
} else {
    Message = Input + " → 数値が入力されました";
}
```
このコードで入力された ```Input``` が数値にできる場合は、数値にしたものが ```input``` という名前で使えるようになります。また数値にできなかった場合は、```if``` と ```else``` の間の
```cs
Message = Input + " → 数字を入力してください";
```
が実行されます。この時、```else``` の次の
```cs
Message = Input + " → 数値が入力されました";
```
は実行されません。数値にできた場合は、
```cs
Message = Input + " → 数字を入力してください";
```
は実行されずに
```cs
Message = Input + " → 数値が入力されました";
```
が実行されます。```if(...){ } else { }``` はこのように条件によってどちら一方だけを実行したい場合に使える機能です。  
**※実際には ```else``` がなかったり ```{ }``` がなかったりという使い方が多いので、```if``` については別途しっかり学んでください。**  

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
    }
}
```
実行し、テキスト入力ボックスに、例えばキーボードで **suuji** と打って **[ 回答 ]** ボタンを押してみましょう。

```
suuji → 数字を入力してください
```
と表示されます。再度実行して今度は **3** と打って **[ 回答 ]** ボタンを押してみましょう。
```
3 → 数値が入力されました
```
と表示されます。

### 入力された文字列が 1 桁の数値であるかを確認する

```if (...){ } else { }``` で条件によって実行されるコードを分岐できることを学びました。  
今度はこの機能をうまく使って、入力された文字列が 1 桁の数値であるかを確認してみましょう。
```cs
if (input < 1 || input > 9)
{
    Message += Input + " → 1-9 の数字を 1 文字入力してください";
}
```
入力された文字列を数値にした ```input``` が、1 より小さいまたは 9 より大きい時だけ **1-9 の数字を 1 文字入力してください** と表示するコードです。 ```input < 1``` といった書き方で、「1 より小さい場合」といった条件を表します。 ```||``` は or 条件です。「1 より小さい」「or」「9 より大きい」場合にだけ ```{ }``` の中のコードが実行されます。  
すなわち、1 ～ 9 の数値 **でない** だけ実行されます。これによりアプリが求める入力の書式に合わないプレイヤーの入力に対して、**「違うよ」** というメッセージをプレイヤーに伝えることができます。  

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
    }
}
```
実行し、キーボードで **12** と打って **[ 回答 ]** ボタンを押してみましょう。
```
12 → 数値が入力されました12 → 1-9 の数字を 1 文字入力してください
```
と表示されます。再度実行してこんどは **3** と打って **[ 回答 ]** ボタンを押してみましょう。  
```
3 → 数値が入力されました
```
と表示されます。

<hr />

[< 前へ](./textbook03.md) | [次へ >](./textbook05.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
<hr />
