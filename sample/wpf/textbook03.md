# WPF アプリを作ってみよう

### 画面の見た目と関連付ける動作を作る

以前に画面の見た目を作った際に、各要素に ```{Binding ``` を書いてアプリの動作のプログラムコードと関連付けを設定していました。
```xml
<StackPanel>
    <TextBlock Text="数字を入力してください"/>
    <TextBox Text="{Binding Input}"/>
    <Button Content="回答" Command="{Binding AnswerCommand}"/>
    <TextBlock Text="{Binding Message}"/>
</StackPanel>
```
```Input```、```AnswerCommand```、```Message``` の関連付ける先のプログラムコードを書いて行きます。

次のコードを ```class MainWindowViewModel : BaseViewModel``` の次の行の ```{``` の次に追加します。
```cs
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
}

void OnAnswerCommand(object parameter)
{
}
```
ここで追加した ```AnswerCommand```、```Input```、```Message``` がそれぞれ画面の ```AnswerCommand```、```Input```、```Message``` と関連付けられます。  
また、このコードで ```AnswerCommand``` と ```void OnAnswerCommand(object parameter)``` も関連付けられます。どういうことかというと、画面のボタンと ```AnswerCommand``` が関連付けられており、```AnswerCommand``` と ```void OnAnswerCommand(object parameter)``` が関連付けられることにより、画面のボタンが押されると、```void OnAnswerCommand(object parameter)``` に書いたプログラムコードが実行されます。  
また、```Message``` も画面の表示と関連付いているため、これらに対し ```Message = "なにかテキスト"``` のようなコードを書くと、そのタイミングで画面の表示も **なにかテキスト** と変わります。  
※```Input``` は逆に画面で **<TextBox>** にユーザーから入力されると、コードの ```Input``` が変わります。  

現在の ```class MainWindowViewModel : BaseViewModel``` の部分は次のようになります。
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
    }

    void OnAnswerCommand(object parameter)
    {
    }
}
```

### ランダムな数字を作る

数字あてゲームのためにはランダムな数字が必要です。  
次のように書いてランダムな数値を作ります。
```cs
_answer = new System.Random().Next(1, 9);
```
この 1 行で、1～9 のランダムが数字が作られ ```_answer``` という名前でプログラムの中で使えるようになります。この 1 行を ```AnswerCommand = new Command(OnAnswerCommand);``` の次に追加します。  

この ```_answer``` という名前のランダムで作られた数字はプログラムで表示させることができます。
```cs
Message = $"{_answer}";
```
この 1 行を ```void OnAnswerCommand(object parameter)``` の次の行の ```{``` の次に追加します。追加した **MainWindow.xaml.cs (の一部)** は次のようになります。  
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
        Message = $"{_answer}";
    }
}
```
実行し、**[ 回答 ]** ボタンを押すと **[ 回答 ]** ボタンの下に
```
{1～9 のランダムな数字}
```
が表示されます。

### 文字を入力する

[ 回答 ] ボタンの上のテキスト入力ボックスはプレイヤーがキーボードから文字を入力できます。ここに入力された文字はプログラムコードの ```Input``` と関連付けられています。そのためプログラムコードでは先ほどの ```_answer``` と同じように、 ```Input``` という名前で使えるようになっています。プログラムコードを書き換えて。テキスト入力ボックスへのプレイヤーの入力を画面に表示してみましょう。  

**変更前**
```cs
Message = $"{_answer}";
```

**変更後**  
```cs
Message = Input;
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
        Message = Input;
    }
}
```
実行し、テキスト入力ボックスに、例えばキーボードで **suuji** と打って **[ 回答 ]** ボタンを押してみましょう。
```
suuji
```
と表示されます。

<hr />

[< 前へ](./textbook02.md) | [次へ >](./textbook04.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
<hr />
