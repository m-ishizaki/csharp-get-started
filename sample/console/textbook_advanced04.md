# 進歩したコンソールアプリを作ってみよう

## 二つのゲームを同時に進行するプログラムコード

ひとまず、**クラス**、**メソッド** を使わずに二つのゲームを進行するプログラムコードが出来上がりました。現在のプログラムコードは次のようになっています。  

```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer1 = new System.Random().Next(1, 9);
            var answer2 = new System.Random().Next(1, 9);
            var cleared1 = false;
            var cleared2 = false;
            int input = default;
            Console.WriteLine("数字あて");
            while (!cleared1 || !cleared2)
            {
                Console.Write("数字を入力: ");
                var line = Console.ReadLine();
                if (!int.TryParse(line, out input))
                {
                    Console.WriteLine("数字を入力してください");
                    continue;
                }
                if (input < 1 || input > 9)
                {
                    Console.WriteLine("1-9 の数字を 1 文字入力してください");
                    continue;
                }
                if(cleared1)
                {
                    Console.WriteLine("ゲーム1: クリア済みです");
                }
                else if(input == answer1)
                {
                    Console.WriteLine("ゲーム1: 正解！ クリアです");
                    cleared1 = true;
                }
                else if (input > answer1)
                {
                    Console.WriteLine("ゲーム1: 答えはもっと小さい値です");
                 }
                else if (input < answer1)
                {
                    Console.WriteLine("ゲーム1: 答えはもっと大きい値です");
                }
                if(cleared2)
                {
                    Console.WriteLine("ゲーム2: クリア済みです");
                }
                else if(input == answer2)
                {
                    Console.WriteLine("ゲーム2: 正解！ クリアです");
                    cleared2 = true;
                }
                else if (input > answer2)
                {
                    Console.WriteLine("ゲーム2: 答えはもっと小さい値です");
                 }
                else if (input < answer2)
                {
                    Console.WriteLine("ゲーム2: 答えはもっと大きい値です");
                }
            }
            Console.Write("正解！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }
}
```

## メソッドを使ってみる

**メソッド** を使ってプログラムコードを改善してみましょう。  
**※このコンテンツは入門者にプログラムコードを書いて動かしてもらうことを目的としているので、メソッド の詳しいことは別途学習してください。**  

**メソッド** を使うとある程度のプログラムの処理をひとまとめにして扱うことができるようになります。わかりやすい例としては、複数回書かれているコードの組を 1 度書くだけで良くなります。例えば今回のコードでは、次の部分が、ゲーム 1 とゲーム 2 用でほぼ同じコードが 2 回書かれています。これを **メソッド** にしてみましょう。  

```cs
if(cleared1)
{
    Console.WriteLine("ゲーム1: クリア済みです");
}
else if(input == answer1)
{
    Console.WriteLine("ゲーム1: 正解！ クリアです");
    cleared1 = true;
}
else if (input > answer1)
{
    Console.WriteLine("ゲーム1: 答えはもっと小さい値です");
 }
else if (input < answer1)
{
    Console.WriteLine("ゲーム1: 答えはもっと大きい値です");
}
if(cleared2)
{
    Console.WriteLine("ゲーム2: クリア済みです");
}
else if(input == answer2)
{
    Console.WriteLine("ゲーム2: 正解！ クリアです");
    cleared2 = true;
}
else if (input > answer2)
{
    Console.WriteLine("ゲーム2: 答えはもっと小さい値です");
 }
else if (input < answer2)
{
    Console.WriteLine("ゲーム2: 答えはもっと大きい値です");
}
```

**メソッド** を追加します。

```cs
bool Proceed(int gameNo, bool cleared, int answer, int input)
{
    Console.Write($"ゲーム{gameNo}: ");
    if (cleared)
    {
        Console.WriteLine("クリア済みです");
        return true;
    }
    if (input > answer)
    {
        Console.WriteLine("答えはもっと小さい値です");
        return false;
    }
    if (input < answer)
    {
        Console.WriteLine("答えはもっと大きい値です");
        return false;
    }
    Console.WriteLine("正解！ クリアです");
    return true;
}
```

ゲーム 1、ゲーム 2 の処理をこの **メソッド** を使うように変更します。

```cs
cleared1 = Proceed(1, cleared1, answer1, input);
cleared2 = Proceed(2, cleared2, answer2, input);
```

<hr />

[< 前へ](./textbook_advanced03.md) | [次へ >](./textbook_advanced05.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)