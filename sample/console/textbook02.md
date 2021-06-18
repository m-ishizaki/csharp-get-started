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
この ```line``` もpログラムで表示させてみましょう。
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





<hr />

[< 前へ](./textbook01.md) | [次へ >](./textbook03.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)