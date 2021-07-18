# 進歩したコンソールアプリを作ってみよう

## クラスを別のファイルに書く

先ほど作った新しいファイル **Game.cs** にクラスのプログラムコードを移動しましょう。  
移動するのは ```Game``` クラスのコードです。  

**変更前**
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
            int input = default;
            Console.WriteLine("数字あて");
            while (!game1.Cleared || !game2.Cleared)
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
                game1.Proceed(input);
                game2.Proceed(input);
            }
            Console.Write("正解！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }

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
}
```

**変更後 Program.cs**
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
            int input = default;
            Console.WriteLine("数字あて");
            while (!game1.Cleared || !game2.Cleared)
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
                game1.Proceed(input);
                game2.Proceed(input);
            }
            Console.Write("正解！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }
}
```

**変更後 Game.cs**
```cs
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

しかし、この状態では **Game.cs** ファイル内でエラーが出ているはずです。

<hr />

[< 前へ](./textbook_advanced06.md) | [次へ >](./textbook_advanced08.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)