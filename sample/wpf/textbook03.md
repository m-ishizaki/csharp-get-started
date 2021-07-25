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

<hr />

[< 前へ](./textbook02.md) | [次へ >](./textbook04.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
<hr />
