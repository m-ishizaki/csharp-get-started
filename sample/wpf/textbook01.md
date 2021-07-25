# WPF アプリを作ってみよう

### 雛形を作る

次の3つのコマンドを実行します。  
```
dotnet new sln -n WpfApp
dotnet new wpf -n WpfApp
dotnet sln add ./WpfApp\WpfApp.csproj
```
これで雛形が作られます。

### Visual Studio Code で開く

雛形のソースコードファイルを Visual Studio Code で開きます。  
次のコマンドを実行します。
```
code .
```
これでソースコードファイル群のディレクトリが Visual Studio Code で開かれます。

### デバッグの設定をする

Visual Studio Code のメニューで次のメニューを選択します。
```
実行 > 構成の追加 > .NET Core
```

初回の選択時に **launch.json** ファイルがが作られ、Visual Studio Code 上で開かれます。

### デバッグ実行をする

Visual Studio Code のメニューで次のメニューを選択します。
```
実行 > デバッグの開始
```
これでプログラムが実行できます。  
しかし今の段階では何もない画面が開くだけです。ここから画面にコントロールと呼ばれる要素を配置して、入力などを行えるようにして行きます。

### 雛形ソースコードのファイルを開く

Visual Studio Code のメニューで次のメニューを選択します。
```
表示 > エクスプローラー
```
Visual Studio Code の左側にエクスプローラー ペインが開きます。その中から **WpfApp** をクリックします。現れるいくつかの選択肢の中から **MainWindow.xaml** をクリックします。すると **MainWindow.xaml** というファイルが Visual Studio Code のメインの領域で開きます。この **MainWindow.xaml** ファイルが画面の要素や配置が書かれるファイルです。  
まずは、このファイルを編集して見た目を作ります。

### 画面の見た目を作る

開いたファイルを次のように変更します。

**変更前**
```xml
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>

    </Grid>
</Window>
```

**変更後**
```xml
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp"
        mc:Ignorable="d"
        Title="数字あて" Height="450" Width="800">
        <Window.DataContext>
            <local:MainWindowViewModel />
        </Window.DataContext>
    <StackPanel>
        <TextBlock Text="数字を入力してください"/>
        <TextBox Text="{Binding Input}"/>
        <Button Content="回答" Command="{Binding AnswerCommand}"/>
        <TextBlock Text="{Binding Message}"/>
    </StackPanel>
</Window>
```

#### 解説
- ```Title="MainWindow"``` → ```Title="数字あて"```  
この部分が画面のタイトルの設定です。
- ```<Window.DataContext><local:MainWindowViewModel /></Window.DataContext>``` (実際は改行が入っています)  
この部分で画面の動作を書くプログラムコードの場所を指定しています。今回は **MainWindowViewModel** という場所に書く、と指定しています。 
- ```<StackPanel>...(途中省略)...</StackPanel>``` (実際は改行が入っています)  
画面の配置の設定です。今回は縦一列に画面の要素が並ぶ指定をしています。この ```<StackPanel>``` と ```</StackPanel>``` に挟まれた要素たちが縦一列に配置されます。  
- ```<TextBlock>```  
画面に文字を表示します。表示するのみで、ユーザーによる入力はできません。 
- ```<TextBox>```  
画面に文字の入力枠を表示します。ユーザーによる入力が可能です。プログラムで文字を入力することもできます。
- ```<Button>```  
画面にボタンを表示します。マウスでクリックするなどが行えるボタンです。  

各要素に書かれている ```{Binding ``` ですが、これはこの後書いていくアプリの動作のプログラムコードと関連付けをする設定です。今回はそれぞれ ```Input```、```AnswerCommand```、```Message``` と関連付ける設定をしています。関連付ける先のプログラムコードは、これから書いて行きます。

### 雛形ソースコードのファイルを開く

画面の動作を書くプログラムコードを **MainWindow.xaml.cs** というファイルに書いていきます。  
**※今回は手順の簡略化のためにこのファイルに書きますが、本来はファイルを新規作成して書く方が望ましいです。そのあたりについては別途しっかり学んでください。**  

Visual Studio Code の左側のエクスプローラー ペインで **MainWindow.xaml.cs** を選択してください。  ファイルが開き、次のようになっています。
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
```

このファイルに画面の動作を書いて行きます。

<hr />

[< 前へ](./textbook.md) | [次へ >](./textbook02.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
