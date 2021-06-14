# コンソールアプリを作ってみよう

### 雛形を作る

次の3つのコマンドを実行します。  
```
dotnet new sln -n ConsoleApp
dotnet new console -n ConsoleApp
dotnet sln add ./ConsoleApp\ConsoleApp.csproj
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

初回の選択時に **launch.json** ファイルがが作られ、Visual Studio Code 上で開かれるます。開かれたファイルの一部を変更します。  
この手順は Visual Studio Code で入力のあるコンソールアプリをデバッグする場合の手順です。どのようなアプリ/実行環境でも常に必要な手順というわけではありません。

**変更前**
```
"console": "internalConsole",
```
**変更後**
```
"console": "externalTerminal",
```

**変更後 launch.json 例**  
変更後の launch.json 全体の例です。
```json
{
    // IntelliSense を使用して利用可能な属性を学べます。
    // 既存の属性の説明をホバーして表示します。
    // 詳細情報は次を確認してください: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (console)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/ConsoleApp/bin/Debug/net5.0/ConsoleApp.dll",
            "args": [],
            "cwd": "${workspaceFolder}/ConsoleApp",
            "console": "externalTerminal",
            "stopAtEntry": false
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach"
        }
    ]
}
```

### デバッグ実行をする

Visual Studio Code のメニューで次のメニューを選択します。
```
実行 > デバッグの開始
```
これでプログラムが実行できます。  
しかし今の段階ではアプリの実行がすぐに終わってしまうので、ユーザーからの入力を待機するプログラムコードを入れて終了前にアプリが待機するようにます。

### 雛形ソースコードのファイルを開く

Visual Studio Code のメニューで次のメニューを選択します。
```
表示 > エクスプローラー
```
Visual Studio Code の左側にエクスプローラー ペインが開きます。その中から **ConsoleApp** をクリックします。  
現れるいくつかの選択肢の中から **Program.cs** をクリックします。今クリックしたのが **Program.cs** というファイルで、雛形で作られたプログラムのソースコードが書かれているファイルです。**Program.cs** ファイルが Visual Studio Code のメインの領域で開きます。  

今回はこのファイルを編集してプログラミングをしていきます。  

### ユーザーの入力を受け付けるコードを追加する

コマンドラインアプリでユーザーからの入力を受け付けるプログラムコードを書き足します。ユーザーからの入力を待つので、アプリがそこでいったん止まります。  
書き足すコードは次の一行です。
```cs
Console.ReadKey();
```
書き足した後の Program.cs ファイルは次のようになります。  
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
```

実行をするとターミナル(またはコマンド プロンプト)が開き、**Hello World!** と出力されています。  
先ほど追加したコードによって、アプリはユーザーからの入力を待機しているので、最初に実行したときと違いアプリは終了しなくなっています。  

Enter キーを押して入力をし、アプリを終了します。

### 出力する言葉を変更する

プログラムの雛形は **Hello World!** という言葉を出力するものになっています。先ほどの実行結果もプログラムの通りに **Hello World!** と表示されました。今度はこの表示される言葉を変えてみましょう。

**変更前**
```cs
Console.WriteLine("Hello World!");
```
**変更後**
```cs
Console.WriteLine("数字あて");
```
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
```

メニューの ```実行 > デバッグの開始``` で実行します。  
ソースコードを書き換えた通りに今度は **数字あて** と表示されます！

<hr />

[< 前へ](./textbook.md) | [次へ >](./textbook02.md)  

[C# でアプリを作るへ](../../textbook/practice.md)