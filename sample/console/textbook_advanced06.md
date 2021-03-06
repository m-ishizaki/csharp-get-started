# 進歩したコンソールアプリを作ってみよう

## 変更後のプログラムコード

**クラス** を使うように変更したプログラムコードはこのようになります。

**変更後**
```cs
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
```

また、```Proceed``` メソッドは **クラス** の中に書いたので、これまでの ```Proceed``` メソッドは削除します。  

## クラスを使ったプログラムコード

ひとまず、**クラス** を使ったプログラムコードが出来上がりました。現在のプログラムコードは次のようになっています。
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

## クラスを別のファイルに書く

**メソッド**、**クラス** を使ってプログラムコードを改善してきました。しかしこれまでの知識だけでは大規模なアプリを作る際に大きな課題に直面します。その課題とは、プログラムコードを一つのファイルに書いていることです。複雑な処理を行うアプリになると、何十万行・何百万行とプログラムコードも大きくなっていきます。それを一つのファイルにすべて書くというのは非現実的です。  
一つのファイルにどのくらいの行数のプログラムコードがかかれるのが適切かは状況によって異なりますが、どのような状況でも一つのファイルが 400 行を超えるようだと何かが間違っていると考えた方が良いです。  

## 新しいファイルを作成する

**Program.cs** と同じディレクトリ(**※1**)に新しいファイルを作成します。作成方法はなんでも良い(**※2**)のですが今回は Visual Studio Code から作ってみます。  

![image](./image0001.png)

- Visual Studio Code のエクスプローラー ペインで **ConsoleApp** を選択します。
- Visual Studio Code のエクスプローラー ペインでマウスカーソルをかさねると「新しいファイル」と表示されるアイコンをクリックします。
- **Game.cs** と入力し、Enter キーを押します。

これでファイルが作られて Visual Studio Code で開かれます。簡単ですね。  

**※1**： 同一ディレクトリでなくとも配下のディレクトリであれば読み込んでくれます。規模が大きくなったらディレクトリで整理をすることになります。  
**※2**： そこにファイルがあれば読み込んでくれるので、他の場所からコピーする・ターミナル(端末)で作る・エクスプローラー/Finder で作る、などどのような方法で作ってもかまいません。  

## ファイル名について

C# のプログラムコードを書くファイルのファイル名は拡張子を **.cs** にする必要がます。名前は自由につけて構いません。  

パソコンを買って買ったままの設定で使っていると、拡張子が見えない状態になっているかもしれません。  
インターネット検索などで、拡張子を表示する方法を探して設定を行っておくのがオススメです。プログラミングを行う際には拡張子が非常に重要で常に意識しておく必要があります。また、日常でパソコンを使っている場合にはあまりないかもしれませんが、拡張子を変更する機会もかなりの頻度で発生します。IT エンジニアがパソコンを買ってまず真っ先に設定するものと言って過言ではない設定の一つです。

<hr />

[< 前へ](./textbook_advanced05.md) | [次へ >](./textbook_advanced07.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)