# 進歩したコンソールアプリを作ってみよう

## 以前のプログラムコード

まずは、[コンソールアプリを作ってみよう](./textbook.md) で作ったプログラムコードを開きます。開き方もリンク先のテキストを参照してください。  
もし、リンク先のコンテンツは読んだだけで理解できたのでいきなり進歩したアプリから始めたいという場合は、リンク先のテキストに **完成サンプルコード** もあります。

**現在のプログラムコード**  
現在のプログラムコードは次のようになっているはずです。  
このコードを **メソッド**、**クラス** という機能を使って進歩させていきます。
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            int input = default;
            Console.WriteLine("数字あて");
            while (input != answer)
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
                if (input > answer)
                {
                    Console.WriteLine("答えはもっと小さい値です");
                    continue;
                }
                if (input < answer)
                {
                    Console.WriteLine("答えはもっと大きい値です");
                    continue;
                }
                break;
            }
            Console.Write("正解！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }
}
```

## 完成コード
いったんはアプリを完成させたことがある経験者が前提となるので、今回は最初に完成コードを一度見ておきましょう。  
[完成サンプルコード](./src_advanced)  
Game.cs
```cs
using System;

internal class Game
{
    int _no { get; init; }
    int _answer { get; init; }
    public bool Cleared { get; private set; }

    public Game(int no)
    {
        _no = no;
        _answer = new System.Random().Next(1, 9);
    }

    public void Proceed(int input)
    {
        Console.Write($"ゲーム{_no}: ");
        if (Cleared)
        {
            Console.WriteLine("クリア済みです");
            return;
        }
        if (input > _answer)
        {
            Console.WriteLine("答えはもっと小さい値です");
            return;
        }
        if (input < _answer)
        {
            Console.WriteLine("答えはもっと大きい値です");
            return;
        }
        Console.WriteLine("正解！ クリアです");
        Cleared = true;
    }
}
```
Program.cs
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game1 = new Game(1);
            var game2 = new Game(2);
            Console.WriteLine("数字あて");
            while (!game1.Cleared || !game2.Cleared)
            {
                Console.Write("数字を入力: ");
                var line = Console.ReadLine();
                if (!int.TryParse(line, out var input))
                {
                    Console.WriteLine("数字を入力してください");
                    continue;
                }
                if (input < 1 || input > 9)
                {
                    Console.WriteLine("1-9 の数字を 1 文字入力してください");
                    continue;
                }
                game1.Proceed(input);
                game2.Proceed(input);
            }
            Console.Write("ゲームクリア！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }
}
```

<hr />

[< 前へ](./textbook_advanced.md) | [次へ >](./textbook_advanced02.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)