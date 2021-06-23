# コンソールアプリを作ってみよう

### 入力された文字列が 1 桁の数値であるかを確認する

```if(...){ } else { }``` で条件によって実行されるコードを分岐できることを学びました。  
今度はこの機能をうまく使って、入力された文字列が 1 桁の数値であるかを確認してみましょう。
```cs
if(input < 1 || input > 9)
{
    Console.WriteLine("1-9 の数字を 1 文字入力してください");
}
```
入力された文字列を数値にした ```input``` が、1 より小さいまたは 9 より大きい時だけ **1-9 の数字を 1 文字入力してください** と表示するコードです。 ```input < 1``` といった書き方で、「1 より小さい場合」といった条件を表します。 ```||``` は or 条件です。「1 より小さい」「or」「9 より大きい」場合にだけ ```{ }``` の中のコードが実行されます。  
すなわち、1 ～ 9 の数値 **でない** だけ実行されます。これによりアプリが求める入力の書式に合わないプレイヤーの入力に対して、「違うよ」というメッセージをプレイヤーに伝えることができます。  
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
            if(!int.TryParse(line, out var input))
            {
                Console.WriteLine("数字を入力してください");
            } else {
                Console.WriteLine("数値が入力されました");
            }
            if(input < 1 || input > 9)
            {
                Console.WriteLine("1-9 の数字を 1 文字入力してください");
            }
            Console.ReadKey();
        }
    }
}
```
```
実行し、キーボードで **12** と打って Enter キーを押してみましょう。
```
12
数値が入力されました
1-9 の数字を 1 文字入力してください
```
と表示されます。再度実行してこんどは **3** と打って Enter キーを押してみましょう。  
```
3
数値が入力されました
```
と表示されます。


<hr />

[< 前へ](./textbook02.md) | [次へ >](./textbook04.md)  

[C# でアプリを作るへ](../../textbook/practice.md)