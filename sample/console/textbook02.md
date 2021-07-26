# コンソールアプリを作ってみよう

### ランダムな数字を作る
数字あてゲームのためにはランダムな数字が必要です。  
次のように書いてランダムな数値を作ります。
```cs
var answer = new System.Random().Next(1, 9);
```
この 1 行で、1～9 のランダムが数字が作られ ```answer``` という名前でプログラムの中で使えるようになります。  
先ほど **数字あて** という言葉を表示したのと同じようにこの ```answer``` という名前のランダムで作られた数字もプログラムで表示させることができます。
```cs
Console.WriteLine(answer);
```
二つのコードを追加した Program.cs は次のようになります。  
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            Console.WriteLine(answer);
            Console.WriteLine("数字あて");
            Console.ReadKey();
        }
    }
}
```
実行すると
```
{1～9 のランダムな数字}
数字あて
```
と表示されます。

### 文字を入力する
プレイヤーがキーボードから文字を入力できる必要があります。  
次のように書いてプログラムが文字の入力を受け付けるようにしましょう。
```cs
var line = Console.ReadLine();
```
この 1 行で、プレイヤーのキーボードからの入力を受け付け ```line``` という名前でプログラムの中で使えるようになります。  
この ```line``` もプログラムで表示させてみましょう。
```cs
Console.WriteLine(line);
```
二つのコードを追加した Program.cs は次のようになります。  
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            Console.WriteLine(answer);
            Console.WriteLine("数字あて");
            var line = Console.ReadLine();
            Console.WriteLine(line);
            Console.ReadKey();
        }
    }
}
```
実行すると
```
{1～9 のランダムな数字}
数字あて
```
が表示されます。ここで例えばキーボードで **suuji** と打って Enter キーを押してみましょう。
```
suuji
```
と表示されます。

### 入力された文字を数値にする

プレイヤーが答えをキーボードを入力するための入力受け付け機能に使うプログラムコードはできましたが、このままでは数字でない文字も入力できてしまいます。そこで受け付ける入力を数値とみなせる文字列だけにしてみます。  
プログラムでは数値と数字はまったくの別物になるので気を付けましょう。例えば、1 という数値と 2 という数値を足すと 1 + 2 = 3 で 3 という数値になります。しかし、数字の場合は 1 + 2 = 12 と一文字 + 一文字 = 二文字の文字列となりまったくの別物です。
今回は C# には文字列を数値にするための機能があるのでそれを使っていきます。数字だけが入力されていれば、その入力は数値にできるので簡易的な入力のチェックができます。
```cs
if (!int.TryParse(line, out var input))
{
    Console.WriteLine("数字を入力してください");
} else {
    Console.WriteLine("数値が入力されました");
}
```
このコードで入力された ```line``` が数値にできる場合は、数値にしたものが ```input``` という名前で使えるようになります。また数値にできなかった場合は、```if``` と ```else``` の間の
```cs
Console.WriteLine("数字を入力してください");
```
が実行されます。この時、```else``` の次の
```cs
Console.WriteLine("数値が入力されました");
```
は実行されません。数値にできた場合は、
```cs
Console.WriteLine("数字を入力してください");
```
は実行されずに
```cs
Console.WriteLine("数値が入力されました");
```
が実行されます。```if(...){ } else { }``` はこのように条件によってどちら一方だけを実行したい場合に使える機能です。  
**※実際には ```else``` がなかったり ```{ }``` がなかったりという使い方が多いので、```if``` については別途しっかり学んでください。**  

**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            Console.WriteLine(answer);
            Console.WriteLine("数字あて");
            var line = Console.ReadLine();
            Console.WriteLine(line);
            if (!int.TryParse(line, out var input))
            {
                Console.WriteLine("数字を入力してください");
            } else {
                Console.WriteLine("数値が入力されました");
            }
            Console.ReadKey();
        }
    }
}
```
実行し、キーボードで **suuji** と打って Enter キーを押してみましょう。
```
suuji
数字を入力してください
```
と表示されます。再度実行して今度は **3** と打って Enter キーを押してみましょう。  
```
3
数値が入力されました
```
と表示されます。

<hr />

[< 前へ](./textbook01.md) | [次へ >](./textbook03.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)